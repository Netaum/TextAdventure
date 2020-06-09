using System.Collections.Generic;

namespace TextAdventure.Interfaces.Entities
{
    public interface INavigator
	{
		Scenes.IScene CurrentScene { get; }
		IEnumerable<Scenes.IScene> SceneExits { get; }
		Scenes.IScene AttemptMoveFromAction(string action);
		void SetNextScene(Scenes.IScene scene);
		void UnpackScene();
		Scenes.IScene GetExitFromCommand(string command);
	}
}