using System.Collections.Generic;
using System.Text;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Controllers
{
	public class Navigator : INavigator
	{
		public IScene CurrentScene { get; private set; }
		private IDictionary<string, IScene> SceneExits;
		private IGameController Controller;

		public string GetSceneDescription()
		{
			var objectDescription = CurrentScene.GetObjectsDescription();
			var exitDescription = CurrentScene.GetExitsDescription();
			var builder = new StringBuilder();

			builder.AppendLine(CurrentScene.GetSceneDescription());
			if (!string.IsNullOrEmpty(objectDescription))
				builder.AppendLine(objectDescription);

			if (!string.IsNullOrEmpty(exitDescription))
				builder.AppendLine(exitDescription);

			return builder.ToString();
		}

		public Navigator(IGameController controller)
		{
			this.Controller = controller;
			SceneExits = new Dictionary<string, IScene>();
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
				SceneExits.Add(exit.Key, exit.Scene);
			}
		}

		public void AttemptToMove(string direction)
		{
			if (SceneExits.ContainsKey(direction))
			{
				MoveScene(SceneExits[direction], "You head off to " + direction);
			}
			else
			{
				Controller.DisplayText("There is no path to " + direction);
			}
		}

		public void AttempChoose(string keyChoose)
		{
			if (SceneExits.ContainsKey(keyChoose))
			{
				MoveScene(SceneExits[keyChoose], "You choice " + keyChoose);
			}
			else
			{
				Controller.DisplayText("The selected choice (" + keyChoose + ") is invalid");
			}
		}

		public void MoveScene(IScene scene,
							  string moveDescription)
		{
			CurrentScene = scene;
			Controller.DisplayText(moveDescription);
			Controller.MovePlayer();
			Controller.StartScene();
		}

		public void ClearSceneExits()
		{
			SceneExits.Clear();
		}
	}
}