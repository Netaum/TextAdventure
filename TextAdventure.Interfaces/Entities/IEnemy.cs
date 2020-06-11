using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Interfaces.Entities
{
    public interface IEnemy
	{
		string Name { get; }
		int Skill { get; }
		int Stamina { get; }
		int CombatTurn { get; }
		int ConsecutiveWinTurns { get; }
		EnemyType EnemyType { get; }
		IList<Conditions.ICondition> CombatConditions { get; }
		void CheckConditions(Controllers.IGameController controller);
		int GetAttack();
		int GetDamage();
		void ReceiveDamage(int damageReceived = 2);
		bool IsDead();
	}
}