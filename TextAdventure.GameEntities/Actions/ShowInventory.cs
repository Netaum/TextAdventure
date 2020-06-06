using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Actions
{
	public class ShowInventory : InputAction, IInputAction
	{
        public ShowInventory()
			:base(ActionEnum.Inventory)
        {
        }
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.Player.InspectInventory();
		}
	}
}