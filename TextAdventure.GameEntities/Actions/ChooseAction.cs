using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Actions
{
    public class ChooseAction: InputAction, IInputAction
	{
        public ChooseAction()
			:base(ActionEnum.Choose)
        {
        }
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.Navigator.AttempChoose(commands[1]);
		}
	}
}