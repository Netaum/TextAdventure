using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Actions
{
	public class ShowStats : InputAction, IInputAction
	{
        public ShowStats()
			:base(ActionEnum.Stats)
        {
        }
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.Player.ShowPlayerStats();
		}
	}
}