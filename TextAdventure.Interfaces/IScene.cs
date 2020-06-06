using System;
using System.Collections.Generic;

namespace TextAdventure.Interfaces
{
	public interface IScene
	{
		string Id { get; }
		string NextScene { get; }
		string Name { get; }
		string Description { get; }
		string ShortDescription { get; }
		IList<IExit> Exits { get; }
		IList<IInteractableObject> Objects { get; }
		IList<ICondition> Conditions { get; }
		bool FileDescription { get; }
		string GetExitsDescription();
		IInteractableObject GetItem(string item);
		string GetObjectsDescription();
		string GetSceneDescription();
		bool ItemExists(string item);
		IResponseAction TryUseItem(string item, IGameController controller);
		IList<IEnemy> Enemies { get; }
	}
}
