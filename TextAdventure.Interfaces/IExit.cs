using System.Collections.Generic;

namespace TextAdventure.Interfaces
{
    public interface IExit
	{
		int Id { get; }
		string SceneId { get; }
		string Description { get; }
		string Key { get; }
		IScene Scene { get; }
		void FindScene(IDictionary<string, IScene> scenes);
	}
}