using TextAdventure.Common.Tools;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
    public class DamageCondition: Condition, ICondition
    {
        public DamageCondition(string value,
                                    string sourceDescription)
        {
            Type = nameof(DamageCondition);
            Value = value;
            SourceDescription = sourceDescription;
        }

        public override void ApplyCondition(IGameController controller)
        {
            int damage = int.Parse(Value);
            controller.DoDamageToPlayer(damage, SourceDescription);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy)
        {
            return true;
        }
    }
}