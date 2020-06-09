using TextAdventure.Common.Tools;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
    public class InventoryCondition: Condition, ICondition
    {
        public InventoryCondition(string checkCondition,
                                  string value)
        {
            Type = nameof(InventoryCondition);
            CheckCondition = Tools.ParseEnum<CheckCondition>(checkCondition);
            Value = value;
        }

        public override void ApplyCondition(IGameController controller)
        {
            if(CheckCondition == CheckCondition.Add)
                controller.AddItemToPlayer(Value);
            
            if(CheckCondition == CheckCondition.Subtract)
                controller.RemoveItemFromPlayer(Value);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy=null)
        {   
            if(CheckCondition == CheckCondition.Add)
            {
                return true;
            }

            if(CheckCondition == CheckCondition.Subtract)
            {
                return controller.PlayerHasItem(Value);
            }

            return false;
        }
    }
}