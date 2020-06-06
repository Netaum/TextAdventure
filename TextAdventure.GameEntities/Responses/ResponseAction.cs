using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Responses
{
	public abstract class ResponseAction : IResponseAction
	{
		public string Description { get; private set; }
		public abstract bool DoResponseAction(IGameController controller);
	}
}