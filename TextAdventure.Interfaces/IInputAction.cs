using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
    public interface IInputAction
	{
		Actions Command { get; }
		void RespondToInput(IGameController controller, string[] commands);
	}
}