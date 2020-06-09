using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
    public interface IInteractableObject
	{
		string Name { get; set; }
		string Description { get; set; }
		IList<IInteraction> Interactions { get; }
		IInteraction GetInteraction(Actions action);
	}
}