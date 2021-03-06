using System.Collections.Generic;
using TextAdventure.Entities;
using TextAdventure.Interfaces.Controllers;
using System.Linq;
using System.Text;
using TextAdventure.Interfaces.Enums;
using TextAdventure.Interfaces.Scenes;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Commands;
using TextAdventure.Conditions;
using TextAdventure.Commands;
using TextAdventure.Common.Tools;

namespace TextAdventure.Controllers
{
	public class GameController : IGameController
	{
		private readonly IList<CheckCondition> changeStatsConditions = new List<CheckCondition>
		{
			CheckCondition.Add,
			CheckCondition.Subtract,
			CheckCondition.Increase,
			CheckCondition.Decrease
		};
		private readonly IDictionary<string, IScene> scenes;
		private readonly IList<IInteractableObject> gameItens;
		private readonly IDisplayController displayController;
		private readonly ILoadController loadController;
		private IEnumerable<IInputCommand> commands;
		public INavigator Navigator { get; private set; }
		public IPlayer Player { get; private set; }

		public IScene CurrentScene
		{
			get
			{
				return Navigator?.CurrentScene;
			}
		}

		public GameController(IDisplayController displayController,
							  ILoadController loadController)
		{
			this.displayController = displayController;
			this.loadController = loadController;
			commands = InputCommand.GetAllCommands();
			scenes = new Dictionary<string, IScene>();
			gameItens = loadController.LoadGameItens("/Scenes/json/game/items.json");
			Navigator = new Navigator(this);
			Player = new Player(this, allItens: gameItens);
			MovePlayer("0", null, null);
		}

		public void StartScene()
		{
			displayController.DisplaySceneDescription(Navigator.CurrentScene);

			if (Navigator.CurrentScene?.Conditions != null)
			{
				foreach (var condition in Navigator.CurrentScene.Conditions)
				{
					if (condition.IsConditionFulfilled(this, null))
					{
						condition.ApplyCondition(this);
					}
				}
			}

			if (!string.IsNullOrEmpty(Navigator.CurrentScene.NextScene))
				MovePlayer(Navigator.CurrentScene.NextScene, null, string.Empty);
		}

		public IInputCommand GetInputCommand(string command)
		{
			var action = Tools.ParseCommand(command);
			if(action.HasValue)
			{
				return commands.SingleOrDefault(f => f.Command == action.Value);
			}
			return null;
		}

		public bool HasEnemies()
		{
			return CurrentScene.Enemies.Any();
		}

		public void PlayerEquipItem(string itemName)
		{
			var equiped = Player.EquipItem(itemName);
			if(equiped)
				displayController.DisplayText($"You equiped {itemName}");
		}
		public void AddCodeword(string codeword)
		{
			Player.CodeWords.Add(codeword);
		}

		public void MovePlayer(string destinationScene,
							   string currentScene,
							   string moveDescription)
		{
			if (!string.IsNullOrEmpty(currentScene) &&
				Navigator.CurrentScene.Id != currentScene)
				return;

			var scene = GetScene(destinationScene);
			MovePlayer(scene, moveDescription);
		}

		private void MovePlayer(IScene scene, string moveDescription)
		{
			Navigator.SetNextScene(scene);
			Player.EnterScene(scene);
			StartScene();
		}

		public void AddItemToPlayer(string itemName)
		{
			Player.AddItem(itemName);
			string description = $"The item '{itemName}' was added to your inventory.";
			displayController.DisplayText(description);
		}

		public void RemoveItemFromPlayer(string itemName)
		{
			var removed = Player.RemoveItem(itemName);
			if (removed)
			{
				string description = $"The item {itemName} was removed to your inventory.";
				displayController.DisplayText(description);
			}
		}

		public void DoDamageToPlayer(int damage, string damageSource)
		{
			var dead = Player.ReceiveDamage(damage);

			var builder = new StringBuilder();
			builder.Append($"You received {damage} damage");
			if (!string.IsNullOrEmpty(damageSource))
				builder.Append($" from {damageSource}");
			builder.Append(".");

			displayController.DisplayText(builder.ToString());

			// Do something is player is dead
		}

		public void ChangePlayerStat(Stats stat, CheckCondition condition, int value)
		{
			if (!changeStatsConditions.Contains(condition))
			{
				throw new System.Exception($"Invalid condition ({condition}) to change stats.");
			}

			string changed = string.Empty;

			switch(condition)
			{
				case CheckCondition.Add:
					Player.IncreaseStat(value, stat);
					changed = "increased";
				break;
				case CheckCondition.Increase:
					Player.IncreaseStat(value, stat, true);
					changed = "raised";
				break;
				case CheckCondition.Subtract:
					Player.DecreaseStat(value, stat);
					changed = "decreased";
				break;
				case CheckCondition.Decrease:
					Player.DecreaseStat(value, stat);
					changed = "reduced";
				break;
			}

			displayController.DisplayText($"You stat {stat} {changed} by {value}.");
		}

		public bool PlayerHasItem(string itemName)
		{
			return Player.HasItem(itemName);
		}

		public void PlayerAttackEnemy()
		{
			var enemy = CurrentScene.Enemies.FirstOrDefault();
			if (enemy == null)
			{
				displayController.DisplayText("There is no enemy to attack.");
				return;
			}

			var result = Player.AttackEnemy(enemy);
			string description = string.Empty;

			switch (result.Item2)
			{
				case AttackResult.NoDamage:
					description = $"You and {enemy.Name} stare at each other, searching an oportunity to attack.";
					break;
				case AttackResult.EnemyDamagesPlayer:
					DoDamageToPlayer(result.Item1, enemy.Name);
					break;
				case AttackResult.PlayerDamagesEnemy:
					enemy.ReceiveDamage(result.Item1);
					description = $"You dealt {result.Item1} to {enemy.Name}.";
					break;
			}

			if (!string.IsNullOrEmpty(description))
			{
				displayController.DisplayText(description);
			}

			if (enemy.IsDead())
			{
				Navigator.CurrentScene.Enemies.Remove(enemy);
				displayController.DisplayText($"You killed the enemy {enemy.Name}");
			}

			foreach (var condition in enemy.CombatConditions)
			{
				if (condition.IsConditionFulfilled(this, enemy))
				{
					condition.ApplyCondition(this);
				}
			}
		}

		public void DisplayEnemyInformation(int? enemyNumber = null)
		{
			if (!CurrentScene.Enemies.Any())
			{
				displayController.DisplayText("There are no enemies here.");
				return;
			}

			if (enemyNumber is null)
			{
				displayController.DisplayEnemyList(CurrentScene.Enemies);
				return;
			}

			if (CurrentScene.Enemies.Count < enemyNumber)
			{
				displayController.DisplayText($"There is no ememy {enemyNumber}.");
				return;
			}

			var enemy = CurrentScene.Enemies[enemyNumber.Value - 1];
			displayController.DisplayEnemyDescription(enemy);
		}

		public void DisplayPlayerInventory()
		{
			displayController.DisplayPlayerInventory(Player);
		}

		public void DisplayPlayerEquipment()
		{
			displayController.DisplayPlayerEquipment(Player);
		}

		public void DisplayPlayerStats()
		{
			displayController.DisplayPlayerStats(Player);
		}

		public void RespondCommandChoice(string choice)
		{
			var exitScene = Navigator.GetExitFromCommand(choice);
			if (string.IsNullOrEmpty(exitScene))
			{
				displayController.DisplayText($"The selected choice ({choice}) is invalid");
				return;
			}
			var scene = GetScene(exitScene);
			MovePlayer(scene, $"You choice {choice}.");
		}

		public void SpawnEnemy(string name,
							   int skill,
							   int stamina,
							   EnemyType type,
							   string nextScene)
		{
			var enemy = new Enemy(name, skill, stamina, type);
			var winCondition = new WinCondition(nextScene);
			enemy.CombatConditions.Add(winCondition);
			CurrentScene.Enemies.Add(enemy);
		}

		public void DisplaySceneExits()
		{
			displayController.DisplayText(CurrentScene.GetExitsDescription());
		}
		private IScene GetScene(string sceneName)
		{
			if (scenes.ContainsKey(sceneName))
			{
				return scenes[sceneName];
			}
			var scene = loadController.LoadScene(sceneName);
			scenes.Add(sceneName, scene);
			return scene;
		}

	}
}