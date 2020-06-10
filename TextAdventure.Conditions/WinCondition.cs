using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;

namespace TextAdventure.Conditions
{
	public class WinCondition : Condition, ICondition
	{
        public WinCondition(string nextScene)
        {
            Type = nameof(AttributeCheckCondition);
            NextScene = nextScene;
        }
		public override void ApplyCondition(IGameController controller)
		{
			controller.MovePlayer(NextScene, null, string.Empty);
		}

		public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy = null)
		{
			if(enemy is null)
                return false;

            return enemy.IsDead() && controller.HasEnemies();
		}
	}
}