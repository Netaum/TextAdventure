using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

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