using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
    public interface IPlayer
	{
		ISheet Sheet { get; }
		IScene CurrentScene { get; }
		void AddItem(IInteractableObject item);
		void EnterScene(IScene scene);
		bool HasItem(string item);
		void InspectInventory();
		void ShowPlayerSheet();
		void ShowPlayerStats();
		IResponseAction TryUseItem(string item);
		void AttackEnemy(IEnemy enemy);
		void ReceiveDamage(int damage = 2, string source = null);
		void ChangeStat(Stats stat, int value);

		void AddItemToInventory(string itemName);
		void RemoveItemFromInventory(string itemName);
	}
}