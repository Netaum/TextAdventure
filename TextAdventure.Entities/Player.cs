using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextAdventure.Common.Tools;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;
using TextAdventure.Interfaces.Scenes;

namespace TextAdventure.Entities
{
	public class Player : IPlayer
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
		private List<IInteractableObject> inventory;
		public IEnumerable<IInteractableObject> Inventory { get { return inventory; } }
		public IDictionary<EquipmentType, IInteractableObject> Equipment { get; private set; }
		private IDictionary<string, IInteractableObject> allDictItems;
		public Player(IGameController controller,
					  bool testChar = false,
					  IList<IInteractableObject> allItens = null)
		{
			this.controller = controller;
			CodeWords = new List<string>();
			NotesClues = new List<string>();
			Equipment = new Dictionary<EquipmentType, IInteractableObject>();

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

			inventory = new List<IInteractableObject>();

			if (allItens != null)
				BuildItemManagement(allItens);

			if (testChar)
				CreateTestChar();
			else CreateNewChar();


		}

		public void EnterScene(IScene scene)
		{
			CurrentScene = scene;
		}

		public bool HasItem(string itemName)
		{
			var item = GetItem(itemName);
			return Inventory.Contains(item);
		}

		public void AddItem(IInteractableObject item)
		{
			if (HasItem(item.Name))
				return;
			inventory.Add(item);
		}

		public void AddItem(string itemName)
		{
			if (HasItem(itemName))
				return;
			var item = GetItem(itemName);
			inventory.Add(item);
		}

		public bool RemoveItem(string itemName)
		{
			if (!HasItem(itemName))
				return false;

			var item = GetItem(itemName);
			inventory.Remove(item);
			return true;
		}

		public bool EquipItem(string itemName)
		{
			if (!HasItem(itemName))
				return false;

			var item = GetItem(itemName);
			if (item.EquipmentType is null)
				return false;

			inventory.Remove(item);
			if (!Equipment.ContainsKey(item.EquipmentType.Value))
			{
				Equipment.Add(item.EquipmentType.Value, item);
			}
			else
			{
				var oldItem = Equipment[item.EquipmentType.Value];
				Equipment[item.EquipmentType.Value] = item;
				inventory.Add(oldItem);
			}

			return true;
		}

		public bool UnequipItem(string itemName)
		{
			if (!HasItem(itemName))
				return false;

			var item = GetItem(itemName);
			if (item.EquipmentType is null)
				return false;

			if (!Equipment.ContainsKey(item.EquipmentType.Value))
			{
				return false;
			}

			inventory.Add(item);
			Equipment.Remove(item.EquipmentType.Value);

			return true;
		}

		private IInteractableObject GetItem(string itemName)
		{
			var key = itemName
					  .Replace(" ", string.Empty)
					  .ToLowerInvariant();
			if (allDictItems.ContainsKey(key))
			{
				return allDictItems[key];
			}

			return null;
		}

		public Interfaces.Interactions.IResponseAction TryDoActionOnItem(PlayerCommands action, string itemName)
		{
			var item = GetItem(itemName);
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

		public void DecreaseStat(int value, Stats stat, bool affectOriginalValues = false)
		{
			var prop = properties[stat];
			var newValue = ((int)prop.GetValue(this)) - value;
			prop.SetValue(this, newValue);

			if (affectOriginalValues && originalStats.Contains(stat))
			{
				var original = originalProperties[stat];
				int newOriginalValue = (int)original.GetValue(this) - value;
				original.SetValue(this, newOriginalValue);
			}
		}

		public void IncreaseStat(int value, Stats stat, bool affectOriginalValues = false)
		{

			if (affectOriginalValues)
			{
				var prop = properties[stat];
				var newPropValue = (int)prop.GetValue(this) + value; 
				prop.SetValue(this, newPropValue);

				var original = originalProperties[stat];
				int newOriginalValue = (int)original.GetValue(this) + value;
				original.SetValue(this, newOriginalValue);
			}
			else
			{
				var prop = properties[stat];
				var propValue = (int)prop.GetValue(this);
				int newValue = propValue + value;

				if (originalStats.Contains(stat))
				{
					var original = originalProperties[stat];
					int originalValue = (int)original.GetValue(this);
					if (newValue > originalValue)
						newValue = originalValue;
				}
				if (newValue != propValue)
					prop.SetValue(this, newValue);
			}

		}

		private void BuildItemManagement(IList<IInteractableObject> allObjects)
		{
			allDictItems = new Dictionary<string, IInteractableObject>();
			foreach (var obj in allObjects)
			{
				var key = obj.Name
							 .Replace(" ", string.Empty)
							 .ToLowerInvariant();
				allDictItems.Add(key, obj);
			}
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

			string[] initialItens = new string[] { "Sword", "Leather Armour", "Lantern", "Tinderbox" };
			string[] initialEquipments = new string[] { "Sword", "Leather Armour" };

			foreach (var stringItem in initialItens)
			{
				var item = GetItem(stringItem);
				AddItem(item);
			}

			foreach (var equipItem in initialEquipments)
			{
				EquipItem(equipItem);
			}
		}
		private void CreateTestChar()
		{
			CreateNewChar(3, 3, 3, 10);
		}


	}
}