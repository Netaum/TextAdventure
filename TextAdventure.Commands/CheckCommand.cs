using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Commands
{
	public class CheckCommand : InputCommand, IInputCommand
	{
		public CheckCommand()
			: base(PlayerCommands.Check)
		{
		}
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			var verb = commands[1];
			int enemyNumber = commands.Length == 3 ?
								int.Parse(commands[2]) :
								1;
			if (verb == "enemy")
			{
				controller.DisplayEnemyInformation(enemyNumber);
			}

			if (verb == "enemies")
			{
				controller.DisplayEnemyInformation();
			}

			if (verb == "inventory")
			{
				controller.DisplayPlayerInventory();
			}

			if (verb == "stats")
			{
				controller.DisplayPlayerStats();
			}
		}
	}
}