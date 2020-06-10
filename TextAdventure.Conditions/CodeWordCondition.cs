using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;

namespace TextAdventure.Conditions
{
	public class CodeWordCondition : Condition, ICondition
	{
        public CodeWordCondition(string codeWord)
        {
            Type = nameof(CodeWordCondition);
            Value = codeWord;
        }
		public override void ApplyCondition(IGameController controller)
		{
			controller.AddCodeword(Value);
		}

		public override bool IsConditionFulfilled(IGameController controller, IEnemy enemy = null)
		{
			return true;
		}
	}
}