using System.Collections.Generic;
using TextAdventure.Interfaces.Enums;
using ICondition = TextAdventure.Interfaces.Conditions.ICondition;
using IGameController = TextAdventure.Interfaces.Controllers.IGameController;

namespace TextAdventure.Interfaces.Entities
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