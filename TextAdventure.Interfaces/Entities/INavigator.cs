using System.Collections.Generic;

namespace TextAdventure.Interfaces.Entities
{
    public interface INavigator
	{
		Scenes.IScene CurrentScene { get; }
		IEnumerable<string> SceneExits { get; }
		string AttemptMoveFromAction(string action);
		void SetNextScene(Scenes.IScene scene);
		void UnpackScene();
		string GetExitFromCommand(string command);
	}
}