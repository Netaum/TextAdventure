using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Scenes
{
    public interface IEnemySpawner
	{
		string Name { get; }
		EnemyType EnemyType { get; }
		string NextScene { get; }
		int NumberOfDice { get; }
		int EnemyCountModifier { get; }
		int Skill { get; }
		int Stamina { get; }
		void SpawnEnemies(Controllers.IGameController controller);
	}
}