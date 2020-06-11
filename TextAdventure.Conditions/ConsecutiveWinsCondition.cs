using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
	public class ConsecutiveWinsCondition : Condition, ICondition
	{
		public ConsecutiveWinsCondition(string checkCondition,
                                        string nextScene,
                                        string value)
		{
			Type = nameof(ConsecutiveWinsCondition);
            CheckCondition = TextAdventure.Common.Tools.Tools.ParseEnum<CheckCondition>(checkCondition);
			Value = value;
            NextScene = nextScene;
		}
		public override void ApplyCondition(IGameController controller)
		{
			controller.MovePlayer(NextScene, null, null);
		}

		public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy = null)
		{
            int numberOfTurns = int.Parse(Value);
			return enemy != null && enemy.ConsecutiveWinTurns >= numberOfTurns;
		}
	}
}