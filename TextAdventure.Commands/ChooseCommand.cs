using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Commands
{
	public class ChooseCommand : InputCommand, IInputCommand
	{
		public ChooseCommand()
			: base(PlayerCommands.Choose)
		{
		}
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.RespondCommandChoice(commands[1]);
		}
	}
}