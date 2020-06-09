using System.Collections.Generic;
using TextAdventure.Entities;
using TextAdventure.Interfaces.Controllers;
using System.Linq;
using System.Text;
using TextAdventure.Interfaces.Enums;
using TextAdventure.Interfaces.Scenes;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Commands;

namespace TextAdventure.Controllers
{
	public class GameController : IGameController
	{
		private readonly IList<CheckCondition> changeStatsConditions = new List<CheckCondition>
		{
			CheckCondition.Add,
			CheckCondition.Subtract
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

			commands = new List<IInputCommand>();

			scenes = new Dictionary<string, IScene>();
			gameItens = new List<IInteractableObject>();
			Navigator = new Navigator(this);
			Player = new Player(this);
		}

		public void StartScene()
		{
			displayController.DisplaySceneDescription(Navigator.CurrentScene);

			if (Navigator.CurrentScene?.Conditions != null)
			{
				foreach (var condition in Navigator.CurrentScene.Conditions)
				{
					//if(condition.IsConditionFulfilled(this, null))
					//{
					//	condition.ApplyCondition(this);
					//}
				}
			}

			if (!string.IsNullOrEmpty(Navigator.CurrentScene.NextScene))
				MovePlayer(Navigator.CurrentScene.NextScene, null, string.Empty);
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
			var description = moveDescription ?? $"You moved to {scene.Name}";
			displayController.DisplayText(description);
		}

		public void AddItemToPlayer(string itemName)
		{
			var item = gameItens.Single(s => s.Name == itemName);
			Player.AddItem(item);
			string description = $"The item {itemName} was added to your inventory.";
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
			if (changeStatsConditions.Contains(condition))
			{
				throw new System.Exception($"Invalid condition ({condition}) to change stats.");
			}

			string changed = condition == CheckCondition.Add ? "increased" : "decreased";

			if (condition == CheckCondition.Add)
				Player.IncreaseStat(value, stat);
			else Player.DecreaseStat(value, stat);

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
		public void DisplayPlayerStats()
		{
			displayController.DisplayPlayerStats(Player);
		}

		public void RespondCommandChoice(string choice)
		{
			var exitScene = Navigator.GetExitFromCommand(choice);
			if (exitScene is null)
			{
				displayController.DisplayText($"The selected choice ({choice}) is invalid");
				return;
			}
			MovePlayer(exitScene, $"You choice {choice}.");
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