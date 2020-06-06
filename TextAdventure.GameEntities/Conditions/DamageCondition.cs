using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Conditions
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
            controller.Player.ReceiveDamage(damage, SourceDescription);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy)
        {
            return true;
        }
    }
}