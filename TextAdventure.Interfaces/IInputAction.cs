using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
    public interface IInputAction
	{
		PlayerCommands Command { get; }
		void RespondToInput(IGameController controller, string[] commands);
	}
}