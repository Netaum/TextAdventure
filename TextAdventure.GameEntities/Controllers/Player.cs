using System;
using System.Linq;
using System.Text;
using TextAdventure.GameEntities.Conditions;
using TextAdventure.Interfaces;
using TextAdventure.Interfaces.Enums;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Controllers
{
	public class Player : IPlayer
	{
		public IScene CurrentScene { get; private set; }
		public ISheet Sheet { get; private set; }
		private IGameController controller;
		// equiped weapon

		public Player(IGameController controller)
		{
			this.controller = controller;
			Sheet = TextAdventure.GameEntities.Controllers.Sheet.RollNewChar();
		}

		public void EnterScene(IScene scene)
		{
			CurrentScene = scene;
		}

		public IResponseAction TryUseItem(string item)
		{
			var actionItem = Sheet.GetItem(item);

			if (actionItem == null)
				return null;

			var action = actionItem.GetInteraction(ActionEnum.Use);

			if (action?.Response == null)
				return null;

			controller.DisplayText(action.ResponseDescription);
			return action.Response;
		}

		public bool HasItem(string item)
		{
			return Sheet.Inventory.Any(a => a.Name == item);
		}

		public void AddItem(IInteractableObject item)
		{
			Sheet.AddItem(item);
		}

		public void InspectInventory()
		{
			controller.DisplayText("You look in your backpack, inside you have: ");
			if (!Sheet.Inventory.Any())
			{
				controller.DisplayText("... nothing.");
				return;
			}

			controller.DisplayText(Sheet.PrintInventory(), 200);
		}

		public void ShowPlayerSheet()
		{
			controller.DisplayText(Sheet.PrintSheet(), 100);
		}

		public void ShowPlayerStats()
		{
			controller.DisplayText(Sheet.PrintStats(), 100);
		}

		public int GetAttack()
		{
			return Sheet.Skill + Common.Tools.StaticRandom.RollDice(2);
		}

		private int GetDamageValue()
		{
			return 2;
		}

		public void AttackEnemy(IEnemy enemy)
		{
			var damageValue = GetDamageValue();
			var enemyAttack = enemy.GetAttack();
			var attack = GetAttack();

			if (attack > enemyAttack)
			{
				enemy.ReceiveDamage(damageValue);
				controller.DisplayText($"You dealt {damageValue} to {enemy.Name}.");
			}
			else if (attack < enemyAttack)
			{
				ReceiveDamage(2, enemy.Name);
			}
			else
			{
				controller.DisplayText($"You and {enemy.Name} stare at each other, searching an oportunity to attack.");
			}
			enemy.CheckConditions(controller);
		}

		public void ReceiveDamage(int damage = 2, string source = null)
		{
			var builder = new StringBuilder();
			builder.Append($"You received {damage} damage");
			if (source != null)
				builder.Append($" from {source}");
			builder.Append(".");

			Sheet.ReceiveDamage(damage);

			controller.DisplayText(builder.ToString());

		}
		public void ChangeStat(Attributes stat, int value)
		{
			string changed = value > 0 ? "increased" : "decreased";
			Sheet.ChangeStat(stat, value);
			controller.DisplayText($"You stat {stat} {changed} by {Math.Abs(value)}.");
		}

		public void AddItemToInventory(string itemName)
		{
			if (Sheet.Inventory.Any(a => a.Name == itemName))
				return;
			var item = controller.GetItem(itemName);
			Sheet.Inventory.Add(item);
		}

		public void RemoveItemFromInventory(string itemName)
		{
			var item = Sheet.Inventory.SingleOrDefault(s => s.Name == itemName);
			if (item == null)
				return;
			Sheet.Inventory.Remove(item);
		}
	}
}