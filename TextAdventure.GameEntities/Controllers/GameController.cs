using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using TextAdventure.Common.Extensions;
using TextAdventure.Common.Tools;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Controllers
{
	public class GameController : IGameController
	{
		private readonly IDictionary<string, IScene> scenes;
		public INavigator Navigator { get; private set; }
		public IPlayer Player { get; private set; }
		public IEnumerable<IInputAction> Actions { get; private set; }

		private IList<IInteractableObject> gameItens;

		public GameController(IDictionary<string, IScene> scenes)
		{
			this.scenes = scenes;

			Navigator = new Navigator(this);
			Player = new Player(this);

			gameItens = new List<IInteractableObject>();

			Actions = new List<IInputAction>
			{
				new GoAction(),
				new ShowInventory(),
				new UseAction(),
				new TakeAction(),
				new ShowSheet(),
				new ShowStats(),
				new ChooseAction(),
				new AttackAction(),
				new CheckAction()
			};
			Navigator.SetNextScene(scenes["0"]);
		}

		public IInputAction GetAction(string command)
		{
			var action = Tools.ParseCommand(command);
			if(action.HasValue)
			{
				return Actions.SingleOrDefault(f => f.Command == action.Value);
			}

			return null;
		}

		public void DisplayText(string text, int waitMilliseconds = 500)
		{
			var lines = text.Split("\n");
			foreach(var l in lines)
			{
				Console.WriteLine(l);
				Thread.Sleep(waitMilliseconds);
			}
		}

		public void StartScene()
		{
			ClearForScene();
			Navigator.UnpackScene();
			DisplayText(Navigator.GetSceneDescription(), 1000);

			foreach(var condition in Navigator.CurrentScene.Conditions)
			{
				if(condition.IsConditionFulfilled(this, null))
				{
					condition.ApplyCondition(this);
				}
			}

			if(!string.IsNullOrEmpty(Navigator.CurrentScene.NextScene))
				MovePlayer(Navigator.CurrentScene.NextScene, null, string.Empty);
		}

		public void TakeItem(string[] command)
		{
			var item = command[1];
			if (!Navigator.CurrentScene.ItemExists(item))
			{
				DisplayText("There is no item " + item + " in the room.");
				return;
			}

			var actionItem = Navigator.CurrentScene.GetItem(item);
			Player.AddItem(actionItem);

			DisplayText("You took " + item);
		}
		public void UseItem(string[] commands)
		{
			var item = commands[1];

			if (!IsItemAvailableToUse(item))
			{
				DisplayText("There is no " + item + " in your inventory or room to use.");
				return;
			}

			var responseAction = Player.TryUseItem(item);

			if (responseAction == null)
			{
				responseAction = Navigator.CurrentScene.TryUseItem(item, this);
			}
			if (responseAction == null)
			{
				DisplayText("You cannot use the " + item);
				return;
			}

			var result = responseAction.DoResponseAction(this);
			if (!result)
				DisplayText("Hmmm. Nothing happens.");
		}

		public void MovePlayer()
		{
			Player.EnterScene(Navigator.CurrentScene);
		}

		public bool MovePlayer(string destinationScene,
							   string currentScene,
							   string moveDescription)
		{
			if(!string.IsNullOrEmpty(currentScene) && Navigator.CurrentScene.Id != currentScene)
				return false;
			var scene = scenes[destinationScene];
			var description = moveDescription ?? $"You moved to {scene.Name}";
			Navigator.MoveScene(scene, description);

			return true;
		}
		private bool IsItemAvailableToUse(string item)
		{
			return Player.HasItem(item) || Navigator.CurrentScene.ItemExists(item);
		}
		private void ClearForScene()
		{
			Navigator.ClearSceneExits();
		}
		public void Start()
		{
			StartScene();
		}

		public void AttackEnemy()
		{
			var enemy = Navigator.CurrentScene.Enemies.FirstOrDefault();
			if(enemy == null)
			{
				DisplayText("There is no enemy to attack.");
				return;
			}

			Player.AttackEnemy(enemy);

			if(enemy.IsDead())
			{
				Navigator.CurrentScene.Enemies.Remove(enemy);
			}
		}

		public void ShowEnemy(int enemyNumber)
		{
			if(!Navigator.CurrentScene.Enemies.Any())
			{
				DisplayText("There are no enemies here.");
				return;
			}
			if(Navigator.CurrentScene.Enemies.Count < enemyNumber)
			{
				DisplayText($"There is no ememy {enemyNumber}.");
				return;
			}
			var enemy = Navigator.CurrentScene.Enemies[enemyNumber-1];
			DisplayText(enemy.PrintStats(), 200);
		}

		public void ShowEnemies()
		{
			if(!Navigator.CurrentScene.Enemies.Any())
			{
				DisplayText("There are no enemies here.");
				return;
			}
			//
			foreach(var (enemy, index) in Navigator.CurrentScene.Enemies.WithIndex())
			{
				DisplayText($"{index + 1} - {enemy.Name}");
			}
		}

		public IInteractableObject GetItem(string itemName)
		{
			return gameItens.SingleOrDefault(s => s.Name == itemName);
		}
	}
}