using System.Collections.Generic;
using TextAdventure.Interfaces;
using System.Linq;

namespace TextAdventure.Entities
{
	public class Navigator
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
		}

		public void UnpackScene()
		{
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

		private void ClearSceneExits()
		{
			sceneExits.Clear();
		}
	}
}