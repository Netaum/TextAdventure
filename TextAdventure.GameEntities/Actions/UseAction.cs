using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;

namespace TextAdventure.GameEntities.Actions
{
	public class UseAction : InputAction, IInputAction
	{
		public UseAction()
			:base(ActionEnum.Use)
		{
		}

		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.UseItem(commands);
		}
	}
}