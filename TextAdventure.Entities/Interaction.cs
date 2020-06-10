
using TextAdventure.Interfaces.Enums;
using TextAdventure.Interfaces.Interactions;

namespace TextAdventure.Entities
{
	public class Interaction : IInteraction
	{
		public PlayerCommands Action { get; set; }
		public string ResponseDescription { get; set; }
		public IResponseAction Response { get; set; }
	}
}