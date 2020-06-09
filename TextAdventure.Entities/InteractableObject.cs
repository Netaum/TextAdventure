using System.Collections.Generic;
using System.Linq;
using TextAdventure.Interfaces;
using TextAdventure.Interfaces.Enums;
namespace TextAdventure.Entities
{
	public class InteractableObject : TextAdventure.Interfaces.Entities.IInteractableObject
	{
		public InteractableObject()
		{
			Interactions = new List<IInteraction>();
			Keywords = new List<string>();
			Damages = new Dictionary<EnemyType, int>();
		}
		public string Name { get; private set; }
		public string Description { get; private set; }
		public string Key { get; private set; }
		public EquipmentType? EquipmentType { get; private set; }
		public IList<IInteraction> Interactions { get; private set; }
		public IList<string> Keywords { get; private set; }
		public IDictionary<EnemyType, int> Damages { get; private set; }
		public IInteraction GetInteraction(PlayerCommands action)
		{
			return Interactions.FirstOrDefault(f => f.Action == action);
		}

	}
}