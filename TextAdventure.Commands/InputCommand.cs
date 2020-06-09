using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Commands
{
	public abstract class InputCommand : IInputCommand
	{
		public InputCommand(PlayerCommands command)
		{
			Command = command;
		}
		public PlayerCommands Command { get; private set; }
		public abstract void RespondToInput(IGameController controller, string[] commands);
	}
}