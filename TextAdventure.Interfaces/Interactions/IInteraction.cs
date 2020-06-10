using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Interactions
{
    public interface IInteraction
	{
		PlayerCommands Action { get; set; }
		string ResponseDescription { get; set; }
		Interactions.IResponseAction Response { get; set; }
	}
}