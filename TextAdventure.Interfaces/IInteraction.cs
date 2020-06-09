using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
    public interface IInteraction
	{
		PlayerCommands Action { get; set; }
		string ResponseDescription { get; set; }
		IResponseAction Response { get; set; }
	}
}