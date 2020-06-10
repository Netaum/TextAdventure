using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Interactions;

namespace TextAdventure.Responses
{
	public abstract class ResponseAction : IResponseAction
	{
		public string Description { get; private set; }
		public abstract bool DoResponseAction(IGameController controller);
	}
}