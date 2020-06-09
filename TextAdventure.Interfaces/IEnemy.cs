using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces
{
	public interface IEnemy
	{
		string Name { get; }
		int Skill { get; }
		int Stamina { get; }
		int CombatTurn { get; }
		EnemyType EnemyType { get; }
		IList<ICondition> MoveConditions { get; }
		void CheckConditions(IGameController controller);
		int GetAttack();
		void ReceiveDamage(int damageReceived = 2);
		bool IsDead();
		string PrintStats();
	}
}