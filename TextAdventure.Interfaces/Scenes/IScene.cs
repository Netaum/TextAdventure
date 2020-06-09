using System.Collections.Generic;

namespace TextAdventure.Interfaces.Scenes
{
    public interface IScene
	{
		string Id { get; }
		string NextScene { get; }
		string Name { get; }
		string Description { get; }
		string ShortDescription { get; }
		IList<Scenes.IExit> Exits { get; }
		IList<Entities.IInteractableObject> Objects { get; }
		IList<Conditions.ICondition> Conditions { get; }
		bool FileDescription { get; }
		string GetExitsDescription();
		Entities.IInteractableObject GetItem(string item);
		string GetObjectsDescription();
		string GetSceneDescription();
		bool ItemExists(string item);
		IResponseAction TryUseItem(string item, Controllers.IGameController controller);
		IList<Entities.IEnemy> Enemies { get; }
	}
}