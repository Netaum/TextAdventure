using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Entities
{
    public interface IInteractableObject
	{
		string Name { get; }
		string Description { get; }
		string Key { get; }
		EquipmentType? EquipmentType { get; }
		IList<IInteraction> Interactions { get; }
		IList<string> Keywords { get; }
		IDictionary<EnemyType, int> Damages { get; }
		IInteraction GetInteraction(PlayerCommands action);
	}
}