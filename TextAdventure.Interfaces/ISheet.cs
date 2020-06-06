using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
	public interface ISheet: IDamageManagement
	{
		int Skill { get; }
		int Stamina { get; }
		int Luck { get; }
		int Provisions { get; }
		int Gold { get; }
		int Change { get; }
		IList<IInteractableObject> Inventory { get; }
		IList<string> CodeWords { get; }
		IList<string> NotesClues { get; }
		IInteractableObject GetItem(string name);
		void AddItem(IInteractableObject item);
        string PrintSheet();
        string PrintInventory();
        string PrintStats();
		void ChangeStat(Attributes stat, int value);
	}
}