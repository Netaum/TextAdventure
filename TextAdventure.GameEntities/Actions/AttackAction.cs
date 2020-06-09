using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;

namespace TextAdventure.GameEntities.Actions
{
	public class AttackAction : InputAction, IInputAction
	{
		public AttackAction()
			:base(ActionEnum.Attack)
		{
		}

		public override void RespondToInput(IGameController controller, string[] commands)
		{
			controller.AttackEnemy();
		}
	}
}