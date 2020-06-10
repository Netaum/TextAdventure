using System.Collections.Generic;
using System.Linq;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Scenes;

namespace TextAdventure.Entities
{
	public class Navigator : INavigator
	{
		public IScene CurrentScene { get; private set; }
		public IEnumerable<IScene> SceneExits
		{
			get
			{
				return sceneExits.Select(s => s.Value);
			}
		}
		private IDictionary<string, IScene> sceneExits;
		private IGameController controller;
		public Navigator(IGameController controller)
		{
			this.controller = controller;
			sceneExits = new Dictionary<string, IScene>();
		}

		public void SetNextScene(IScene scene)
		{
			CurrentScene = scene;
			ClearSceneExits();
			UnpackScene();
		}

		public void UnpackScene()
		{
			if(CurrentScene.EnemySpawner != null)
				CurrentScene.EnemySpawner.SpawnEnemies(controller);

			if (CurrentScene.Exits == null)
				return;

			foreach (var exit in CurrentScene.Exits)
			{
				sceneExits.Add(exit.Key, exit.Scene);
			}
		}

		public IScene AttemptMoveFromAction(string action)
		{
			return sceneExits.ContainsKey(action) ?
				   sceneExits[action] :
				   null;
		}

		public IScene GetExitFromCommand(string command)
		{
			if(!sceneExits.ContainsKey(command))
				return null;
			
			return sceneExits[command];
		}

		private void ClearSceneExits()
		{
			sceneExits.Clear();
		}
	}
}