using System.Collections.Generic;

namespace TextAdventure.Interfaces.Controllers
{
    public interface IDisplayController
	{
		void DisplayText(string text, int waitMilliseconds = 500);
		void DisplaySceneDescription(Scenes.IScene scene);
		void DisplayEnemyDescription(Entities.IEnemy enemy);
		void DisplayEnemyList(IEnumerable<Entities.IEnemy> enemies);
		void DisplayPlayerInventory(Entities.IPlayer player);
		void DisplayPlayerStats(Entities.IPlayer player);
	}
}