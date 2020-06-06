using System.Collections.Generic;
using System.Linq;
using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Items
{
	public class InteractableObject : IInteractableObject
	{
		public InteractableObject()
		{
			Interactions = new List<IInteraction>();
		}
		public string Name { get; set; }
		public string Description { get; set; }
		public IList<IInteraction> Interactions { get; private set; }

		public IInteraction GetInteraction(ActionEnum action)
		{
			return Interactions.FirstOrDefault(f => f.Action == action);
		}

	}
}