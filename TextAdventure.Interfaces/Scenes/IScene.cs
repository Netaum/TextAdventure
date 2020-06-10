using System.Collections.Generic;

namespace TextAdventure.Interfaces.Scenes
{
    public interface IScene
	{
		string Id { get; }
		string NextScene { get; }
		string Name { get; }
		string Description { get; }
		IList<Scenes.IExit> Exits { get; }
		IList<Entities.IInteractableObject> Objects { get; }
		IList<Conditions.ICondition> Conditions { get; }
		IList<Entities.IEnemy> Enemies { get; }
		string GetExitsDescription();
		string GetObjectsDescription();
	}
}