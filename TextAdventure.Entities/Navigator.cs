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
		public IEnumerable<string> SceneExits
		{
			get
			{
				return sceneExits.Select(s => s.Value);
			}
		}
		private IDictionary<string, string> sceneExits;
		private IGameController controller;
		public Navigator(IGameController controller)
		{
			this.controller = controller;
			sceneExits = new Dictionary<string, string>();
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
				sceneExits.Add(exit.Key, exit.SceneId);
			}
		}

		public string AttemptMoveFromAction(string action)
		{
			return sceneExits.ContainsKey(action) ?
				   sceneExits[action] :
				   null;
		}

		public string GetExitFromCommand(string command)
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