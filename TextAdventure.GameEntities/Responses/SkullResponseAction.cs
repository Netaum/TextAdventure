using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Responses
{
	public class SkullResponseAction : ResponseAction, IResponseAction
	{
		public override bool DoResponseAction(IGameController controller)
		{
			return false;
		}
	}
}