
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Items
{
	public class Interaction : IInteraction
	{
		public Interfaces.Enums.PlayerCommands Action { get; set; }
		public string ResponseDescription { get; set; }
		public IResponseAction Response { get; set; }
	}
}