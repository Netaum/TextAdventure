
using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Commands
{
	public class AttackCommand : InputCommand, IInputCommand
	{
		public AttackCommand()
			:base(PlayerCommands.Attack)
		{
		}

		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.PlayerAttackEnemy();
		}
	}
}