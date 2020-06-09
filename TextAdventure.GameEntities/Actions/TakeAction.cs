using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Actions
{
	public class TakeAction : InputAction, IInputAction
	{
        public TakeAction()
			:base(ActionEnum.Take)
        {
        }
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.TakeItem(commands);
		}
	}
}