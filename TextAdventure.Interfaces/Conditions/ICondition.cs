using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Conditions
{
	public interface ICondition
	{
		string Type { get; }
		Stats Attribute { get; }
		CheckCondition CheckCondition { get; }
		string Value { get; }
		string NextScene { get; }
		string SourceDescription { get; }
		bool IsConditionFulfilled(Controllers.IGameController controller, Entities.IEnemy enemy = null);
		void ApplyCondition(Controllers.IGameController controller);
	}
}