using System.Collections.Generic;
using System.Text;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Entities
{
	public class Enemy : IEnemy
	{
		public string Name { get; private set; }
		public int Skill { get; private set; }
		public int Stamina { get; private set; }
		public int CombatTurn { get; private set; }
		public IList<ICondition> CombatConditions { get; private set; }
		public EnemyType EnemyType { get; private set; }

		public Enemy(string name,
					 int skill,
					 int stamina,
					 EnemyType type)
		{
			Name = name;
			Skill = skill;
			Stamina = stamina;
			EnemyType = type;
			CombatTurn = 0;
			CombatConditions = new List<ICondition>();
		}

		public int GetAttack()
		{
			CombatTurn++;
			return Skill + Common.Tools.StaticRandom.RollDice(2);
		}

		public void CheckConditions(IGameController controller)
		{
			foreach (var condition in CombatConditions)
			{
				if (condition.IsConditionFulfilled(controller, this))
				{
					condition.ApplyCondition(controller);
					break;
				}
			}
		}

		public void ReceiveDamage(int damageReceived = 2)
		{
			Stamina -= damageReceived;
		}

        public bool IsDead()
        {
            return Stamina <= 0;
        }

		public string PrintStats()
        {
            var builder = new StringBuilder();
			builder.AppendLine($"NAME: {Name}");
			builder.AppendLine($"SKILL: {Skill}");
			builder.AppendLine($"STAMINA: {Stamina}");
            return builder.ToString();
        }
	}
}