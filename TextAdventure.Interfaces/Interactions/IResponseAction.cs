namespace TextAdventure.Interfaces.Interactions
{
    public interface IResponseAction
	{
		string Description { get; }

		bool DoResponseAction(Controllers.IGameController controller);
	}
}