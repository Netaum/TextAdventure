using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;

namespace TextAdventure.GameEntities.Actions
{
	public class GoAction : InputAction, IInputAction
	{
        public GoAction()
			:base(ActionEnum.Go)
        {
        }
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.Navigator.AttemptToMove(commands[1]);
		}
	}
}