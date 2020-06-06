namespace TextAdventure.Interfaces
{
    public interface INavigator
	{
		IScene CurrentScene { get; }
		void AttemptToMove(string direction);
		void ClearSceneExits();
		string GetSceneDescription();
		void SetNextScene(IScene scene);
		void UnpackScene();
		void MoveScene(IScene scene,string moveDescription);
		void AttempChoose(string keyChoose);
	}
}