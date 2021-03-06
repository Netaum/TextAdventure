using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Conditions
{
    public abstract class Condition : ICondition
    {
        public string Type { get; protected set; }
        public Stats Attribute { get; protected set; }
        public CheckCondition CheckCondition { get; protected set; }
        public string Value { get; protected set; }
        public string NextScene { get; protected set; }
		public string SourceDescription { get; protected set; }

        public abstract bool IsConditionFulfilled(IGameController controller, IEnemy enemy = null);
        public abstract void ApplyCondition(IGameController controller);
	}
}