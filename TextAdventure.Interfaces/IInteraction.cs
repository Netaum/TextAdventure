using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
    public interface IInteraction
	{
		Actions Action { get; set; }
		string ResponseDescription { get; set; }
		IResponseAction Response { get; set; }
	}
}