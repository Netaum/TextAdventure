using System;
using TextAdventure.Common.Tools;
using TextAdventure.Interfaces;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.GameEntities.Conditions
{
    public class AttributeCheckCondition: Condition, ICondition
    {
        public AttributeCheckCondition(string attribute,
                                       string checkCondition,
                                       string value,
                                       string nextScene)
        {
            Type = nameof(AttributeCheckCondition);
            Attribute = Tools.ParseEnum<Attributes>(attribute);
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
                case Attributes.CombatTurn:
                    attributeValue = enemy.CombatTurn;
                    break;
                case Attributes.Stamina:
                    attributeValue = enemy.Stamina;
                    break;
                case Attributes.Skill:
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