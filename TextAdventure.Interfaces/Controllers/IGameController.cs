using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Controllers
{
	public interface IGameController
	{
		void MovePlayer(string destinationScene, string currentScene, string moveDescription);
		void AddItemToPlayer(string itemName);
		void RemoveItemFromPlayer(string itemName);
		void DoDamageToPlayer(int damage, string damageDescription);
		void ChangePlayerStat(Stats stat, CheckCondition condition, int value);
		void AddCodeword(string codeword);
		bool PlayerHasItem(string itemName);
		bool HasEnemies();
		void PlayerAttackEnemy();
		void StartScene();
		void DisplayEnemyInformation(int? enemy = null);
		void SpawnEnemy(string name, int skill, int stamina, EnemyType type, string nextScene);
		void DisplayPlayerInventory();
		void DisplayPlayerEquipment();
		void DisplayPlayerStats();
		void RespondCommandChoice(string choice);
		Scenes.IScene CurrentScene { get; }
		IInputCommand GetInputCommand(string command);
		void DisplaySceneExits();
		void PlayerEquipItem(string itemName);
	}
}