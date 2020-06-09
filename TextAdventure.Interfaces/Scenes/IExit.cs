using System.Collections.Generic;

namespace TextAdventure.Interfaces.Scenes
{
    public interface IExit
	{
		int Id { get; }
		string SceneId { get; }
		string Description { get; }
		string Key { get; }
		Scenes.IScene Scene { get; }
		void FindScene(IDictionary<string, Scenes.IScene> scenes);
	}
}