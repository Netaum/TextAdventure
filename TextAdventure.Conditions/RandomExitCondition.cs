using TextAdventure.Common.Tools;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
    public class RandomExitCondition: Condition, ICondition
    {
        public RandomExitCondition(string value)
        {
            Type = nameof(RandomExitCondition);
            Value = value;
        }

        public override void ApplyCondition(IGameController controller)
        {
            var exits = Value.Split(",");
            var randomExit = TextAdventure.Common.Tools.StaticRandom.Instance.Next(exits.Length);

            controller.MovePlayer(exits[randomExit], null, null);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy)
        {
            return true;
        }
    }
}