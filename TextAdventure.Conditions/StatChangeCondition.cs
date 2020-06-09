using TextAdventure.Common.Tools;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
	public class StatChangeCondition: Condition, ICondition
    {
        public StatChangeCondition(string attribute,
                                   string checkCondition,
                                   string value)
        {
            Type = nameof(StatChangeCondition);
            Attribute = Tools.ParseEnum<Stats>(attribute);
            CheckCondition = Tools.ParseEnum<CheckCondition>(checkCondition);
            Value = value;
        }

        public override void ApplyCondition(IGameController controller)
        {
            int intValue = int.Parse(Value);
            controller.ChangePlayerStat(Attribute, CheckCondition, intValue);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy)
        {
            return true;
        }
    }
}