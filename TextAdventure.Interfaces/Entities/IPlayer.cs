using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Entities
{
    public interface IPlayer
	{
		int Skill { get; }
		int Stamina { get; }
		int Luck { get; }
		int Provisions { get; }
		int Gold { get; }
		int Change { get; }
		Scenes.IScene CurrentScene { get; }
		IList<string> CodeWords { get; }
		IList<string> NotesClues { get; }
		IList<Entities.IInteractableObject> Inventory { get; }
		IDictionary<EquipmentType, Entities.IInteractableObject> Equipment { get; }
		void AddItem(Entities.IInteractableObject item);
		(int, AttackResult) AttackEnemy(Entities.IEnemy enemy, int? attackRoll = null);
		void DecreaseStat(int value, Stats stat);
		void EnterScene(Scenes.IScene scene);
		bool EquipItem(string itemName);
		bool HasItem(string item);
		void IncreaseStat(int value, Stats stat);
		bool ReceiveDamage(int damage);
		bool RemoveItem(string itemName);
		IResponseAction TryDoActionOnItem(PlayerCommands action, string itemName);
	}
}