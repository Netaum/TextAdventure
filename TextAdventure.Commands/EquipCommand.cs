
using System.Linq;
using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Commands
{
	public class EquipCommand : InputCommand, IInputCommand
	{
		public EquipCommand()
			:base(PlayerCommands.Equip)
		{
		}

		public override void RespondToInput(IGameController controller, string[] commands)
		{
			string equipName = string.Join(" ", commands.Skip(1));
			controller.PlayerEquipItem(equipName);
		}
	}
}