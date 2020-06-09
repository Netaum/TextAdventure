using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;

namespace TextAdventure.GameEntities.Actions
{
	public class ShowSheet : InputAction, IInputAction
	{
		public ShowSheet()
			: base(ActionEnum.Sheet)
		{
		}
		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.Player.ShowPlayerSheet();
		}
	}
}