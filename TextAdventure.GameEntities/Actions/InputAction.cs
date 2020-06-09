using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;

namespace TextAdventure.GameEntities.Actions
{
	public abstract class InputAction : IInputAction
	{
		public InputAction(ActionEnum action)
		{
			Command = action;
		}
		public ActionEnum Command { get; private set; }
		public abstract void RespondToInput(IGameController controller, string[] commands);
	}
}