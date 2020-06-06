using TextAdventure.Common.Tools;
using TextAdventure.Interfaces;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.GameEntities.Conditions
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
                controller.Player.AddItemToInventory(Value);
            
            if(CheckCondition == CheckCondition.Subtract)
                controller.Player.RemoveItemFromInventory(Value);
        }

        public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy=null)
        {   
            if(CheckCondition == CheckCondition.Add)
            {
                return true;
            }

            if(CheckCondition == CheckCondition.Subtract)
            {
                return controller.Player.HasItem(Value);
            }

            return false;
        }
    }
}