using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
	public interface ICondition
	{
		string Type { get; }
		Attributes Attribute { get; }
		CheckCondition CheckCondition { get; }
		string Value { get; }
		string NextScene { get; }
		string SourceDescription { get; }
		bool IsConditionFulfilled(IGameController controller, IEnemy enemy = null);
		void ApplyCondition(IGameController controller);
	}
}