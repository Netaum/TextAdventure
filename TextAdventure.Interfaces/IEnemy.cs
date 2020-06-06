using System.Collections.Generic;

namespace TextAdventure.Interfaces
{
    public interface IEnemy
	{
		string Name { get; }
		int Skill { get; }
		int Stamina { get; }
		int CombatTurn { get; }
		// type of enemy
		IList<ICondition> MoveConditions { get; }
		void CheckConditions(IGameController controller);
		int GetAttack();
		void ReceiveDamage(int damageReceived = 2);
		bool IsDead();
		string PrintStats();
	}
}