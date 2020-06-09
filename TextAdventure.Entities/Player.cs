using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextAdventure.Common.Tools;
using TextAdventure.Interfaces;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Entities
{
	public class Player
	{
		private IList<Stats> originalStats = new List<Stats> { Stats.Stamina, Stats.Luck, Stats.Skill };
		private IDictionary<Stats, PropertyInfo> properties;
		private IDictionary<Stats, FieldInfo> originalProperties;
		private const int DEFAULT_DAMAGE = 2;
		private int originalSkill;
		private int originalStamina;
		private int originalLuck;
		private IGameController controller;
		public int Skill { get; private set; }
		public int Stamina { get; private set; }
		public int Luck { get; private set; }
		public int Provisions { get; private set; }
		public int Gold { get; private set; }
		public int Change { get; private set; }
		public IScene CurrentScene { get; private set; }
		public IList<string> CodeWords { get; private set; }
		public IList<string> NotesClues { get; private set; }
		public IList<TextAdventure.Entities.IInteractableObject> Inventory { get; private set; }
		public IDictionary<EquipmentType, TextAdventure.Entities.IInteractableObject> Equipment { get; private set; }

		public Player(IGameController controller,
					  bool testChar = false)
		{
			this.controller = controller;
			CodeWords = new List<string>();
			NotesClues = new List<string>();
			Inventory = new List<TextAdventure.Entities.IInteractableObject>();
			Equipment = new Dictionary<EquipmentType, TextAdventure.Entities.IInteractableObject>();

			var type = this.GetType();

			properties = new Dictionary<Stats, PropertyInfo>
			{
				{Stats.Skill, type.GetProperty(nameof(Skill)) },
				{Stats.Stamina, type.GetProperty(nameof(Stamina)) },
				{Stats.Luck, type.GetProperty(nameof(Luck)) },
				{Stats.Change, type.GetProperty(nameof(Change)) },
				{Stats.Provision, type.GetProperty(nameof(Provisions)) },
				{Stats.Gold, type.GetProperty(nameof(Gold)) },
			};

			originalProperties = new Dictionary<Stats, FieldInfo>
			{
				{Stats.Skill, type.GetField(nameof(originalSkill), BindingFlags.NonPublic | BindingFlags.Instance) },
				{Stats.Stamina, type.GetField(nameof(originalStamina), BindingFlags.NonPublic | BindingFlags.Instance) },
				{Stats.Luck, type.GetField(nameof(originalLuck), BindingFlags.NonPublic | BindingFlags.Instance) },
			};

			if (testChar)
				CreateTestChar();
			else CreateNewChar();
		}

		public void EnterScene(IScene scene)
		{
			CurrentScene = scene;
		}

		public bool HasItem(string item)
		{
			return Inventory.Any(a => a.Name == item);
		}

		public void AddItem(IInteractableObject item)
		{
			if (HasItem(item.Name))
				return;
			Inventory.Add(item);
		}

		public bool RemoveItem(string itemName)
		{
			if (!HasItem(itemName))
				return false;

			var item = Inventory.Single(s => s.Name == itemName);
			Inventory.Remove(item);

			if (item.EquipmentType != null && Equipment.ContainsKey(item.EquipmentType.Value))
				Equipment.Remove(item.EquipmentType.Value);

			return true;
		}

		public bool EquipItem(string itemName)
		{
			if (!HasItem(itemName))
				return false;

			var item = Inventory.Single(s => s.Name == itemName);
			if (item.EquipmentType is null)
				return false;

			if (!Equipment.ContainsKey(item.EquipmentType.Value))
			{
				Equipment.Add(item.EquipmentType.Value, item);
			}
			else
			{
				Equipment[item.EquipmentType.Value] = item;
			}
			return true;
		}

		public IResponseAction TryDoActionOnItem(Actions action, string itemName)
		{
			var item = Inventory.FirstOrDefault(f => f.Name == itemName);
			if (item == null)
				return null;

			var actionResult = item.GetInteraction(action);
			if (actionResult?.Response == null)
				return null;

			return actionResult.Response;
		}

		public (int, AttackResult) AttackEnemy(IEnemy enemy, int? attackRoll = null)
		{
			var attack = attackRoll ?? GetAttack();
			var enemyAttack = enemy.GetAttack();

			if (attack == enemyAttack)
				return (0, AttackResult.NoDamage);

			if (attack > enemyAttack)
			{
				var damage = GetDamage(enemy);
				return (damage, AttackResult.PlayerDamagesEnemy);
			}

			//var enemyDamage = enemy.GetDamage(this);
			int enemyDamage = 2;
			return (enemyDamage, AttackResult.EnemyDamagesPlayer);
		}
		
		public bool ReceiveDamage(int damage)
		{
			Stamina -= damage;
			return Stamina <= 0;
		}
		
		public void DecreaseStat(int value, Stats stat)
		{
			var prop = properties[stat];
			var newValue = ((int)prop.GetValue(this)) - value;
			prop.SetValue(this, newValue);
		}
		
		public void IncreaseStat(int value, Stats stat)
		{
			var prop = properties[stat];
			var propValue = (int) prop.GetValue(this);
			int newValue = propValue + value;

			if(originalStats.Contains(stat))
			{
				var original = originalProperties[stat];
				int originalValue = (int) original.GetValue(this);
				if(newValue > originalValue)
					newValue = originalValue;
			}
			if(newValue != propValue)
				prop.SetValue(this, newValue);
		}


		private int GetAttack()
		{
			return Skill + Common.Tools.StaticRandom.RollDice(2);
		}
		
		private int GetDamage(IEnemy enemy)
		{
			Equipment.TryGetValue(EquipmentType.Weapon, out var item);
			if (item == null)
				return DEFAULT_DAMAGE;

			if (item.Damages.TryGetValue(enemy.EnemyType, out var itemDamage))
				return itemDamage;

			return item.Damages[EnemyType.Default];
		}
		
		private void CreateNewChar(int? skillValue = null,
								   int? staminaValue = null,
								   int? luckValue = null,
								   int? goldValue = null)
		{
			int skill = skillValue ?? StaticRandom.Instance.Next(1, 4) + 7;
			originalSkill = skill;
			Skill = skill;

			int stamina = staminaValue ?? StaticRandom.Instance.Next(2, 13) + 10;
			originalStamina = stamina;
			Stamina = stamina;

			int luck = luckValue ?? StaticRandom.Instance.Next(1, 7) + 6;
			originalLuck = luck;
			Luck = luck;

			Change = 0;
			Gold = goldValue ?? StaticRandom.Instance.Next(2, 13) + 6;

			Provisions = 0;
		}
		private void CreateTestChar()
		{
			CreateNewChar(3, 3, 3, 10);
		}

		
	}
}