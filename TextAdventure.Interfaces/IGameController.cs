using System.Collections.Generic;

namespace TextAdventure.Interfaces
{
	public interface IGameController
	{
		INavigator Navigator { get; }
		IPlayer Player { get; }
		IEnumerable<IInputAction> Actions { get; }
		void StartScene();
		void DisplayText(string text, int waitMilliseconds = 500);
		IInputAction GetAction(string command);
		void MovePlayer();
		void Start();
		void TakeItem(string[] command);
		void UseItem(string[] commands);
		bool MovePlayer(string destinationScene, string currentScene, string moveDescription);
		void AttackEnemy();
		void ShowEnemy(int enemyNumber);
		void ShowEnemies();
		IInteractableObject GetItem(string itemName);
	}
}