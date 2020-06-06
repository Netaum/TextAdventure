namespace TextAdventure.Interfaces
{
    public interface IResponseAction
	{
		string Description { get; }

		bool DoResponseAction(IGameController controller);
	}
}