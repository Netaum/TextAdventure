using TextAdventure.Common.Tools;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
    public class AttributeCheckCondition: Condition, ICondition
    {
        public AttributeCheckCondition(string attribute,
                                       string checkCondition,
                                       string value,
                                       string nextScene)
        {
            Type = nameof(AttributeCheckCondition);
            Attribute = Tools.ParseEnum<Stats>(attribute);
            CheckCondition = Tools.ParseEnum<CheckCondition>(checkCondition);
            Value = value;
            NextScene = nextScene;
        }

        public override void ApplyCondition(IGameController controller)
        {
            controller.MovePlayer(NextScene, null, string.Empty);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy)
        {
            int value = int.Parse(this.Value);
            int attributeValue = 0;
            switch(Attribute)
            {
                case Stats.CombatTurn:
                    attributeValue = enemy.CombatTurn;
                    break;
                case Stats.Stamina:
                    attributeValue = enemy.Stamina;
                    break;
                case Stats.Skill:
                    attributeValue = enemy.Skill;
                    break;
            }
            bool result = false;
            switch(CheckCondition)
            {
                case CheckCondition.Less:
                    result = attributeValue < value;
                    break;
                case CheckCondition.LessOrEqual:
                    result = attributeValue <= value;
                    break;

                case CheckCondition.Equal:
                    result = attributeValue == value;
                    break;

                case CheckCondition.Greater:
                    result = attributeValue > value;
                    break;

                case CheckCondition.GreaterOrEqual:
                    result = attributeValue >= value;
                    break;

                default:
                    result = false;
                    break;
            }

            return result;
        }
    }
}