using System.Collections.Generic;
using TextAdventure.Common.Tools;
using TextAdventure.Interfaces;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.GameEntities.Conditions
{
    public class StatChangeCondition: Condition, ICondition
    {
        public StatChangeCondition(string attribute,
                                   string checkCondition,
                                   string value)
        {
            Type = nameof(StatChangeCondition);
            Attribute = Tools.ParseEnum<Attributes>(attribute);
            CheckCondition = Tools.ParseEnum<CheckCondition>(checkCondition);
            Value = value;
        }

        public override void ApplyCondition(IGameController controller)
        {
            int intValue = int.Parse(Value);
            if(CheckCondition == CheckCondition.Subtract)
                intValue *= -1;
            controller.Player.ChangeStat(Attribute, intValue);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy)
        {
            return true;
        }
    }
}