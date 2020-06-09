using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Commands
{
    public interface IInputCommand
	{
		PlayerCommands Command { get; }
		void RespondToInput(TextAdventure.Interfaces.Controllers.IGameController controller, string[] commands);
	}
}