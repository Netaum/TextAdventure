using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;
using TextAdventure.Interfaces.Scenes;

namespace TextAdventure.Scenes
{
	public class EnemySpawner : IEnemySpawner
	{
		public string Name { get; private set; }
		public EnemyType EnemyType { get; private set; }
		public string NextScene { get; private set; }
		public int NumberOfDice { get; private set; }
		public int EnemyCountModifier { get; private set; }
		public int Skill { get; private set; }
		public int Stamina { get; private set; }

		public EnemySpawner(string name,
							EnemyType type,
							string nextScene,
							int numberOfDice,
							int enemyCountModifier,
							int skill,
							int stamina)
		{
			Name = name;
			EnemyType = type;
			NextScene = nextScene;
			NumberOfDice = numberOfDice;
			EnemyCountModifier = enemyCountModifier;
			Skill = skill;
			Stamina = stamina;
		}

		public void SpawnEnemies(IGameController controller)
		{
			int numberOfEnemies = TextAdventure.Common.Tools.StaticRandom.RollDice(NumberOfDice) + EnemyCountModifier;
			for (int i = 1; i <= numberOfEnemies; i++)
			{
				controller.SpawnEnemy(Name, Skill, Stamina, EnemyType, NextScene);
			}
		}

	}
}