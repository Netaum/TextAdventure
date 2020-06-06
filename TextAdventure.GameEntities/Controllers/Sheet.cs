using System.Collections.Generic;
using TextAdventure.Interfaces;
using System.Linq;
using TextAdventure.Common.Tools;
using TextAdventure.GameEntities.Items;
using ActionEnum = TextAdventure.Interfaces.Enums;
using System.Text;
using System;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.GameEntities.Controllers
{
	public class Sheet : ISheet
	{
		private int originalSkill;
		private int originalStamina;
		private int originalLuck;

		public int Skill { get; private set; }
		public int Stamina { get; private set; }
		public int Luck { get; private set; }
		public int Provisions { get; private set; }
		public int Gold { get; private set; }
		public int Change { get; private set; }
		public IList<string> CodeWords { get; private set; }
		public IList<string> NotesClues { get; private set; }
		public IList<IInteractableObject> Inventory { get; private set; }

		public Sheet()
		{
			CodeWords = new List<string>();
			NotesClues = new List<string>();
			Inventory = new List<IInteractableObject>();
		}

		public IInteractableObject GetItem(string name)
		{
			return Inventory.FirstOrDefault(f => f.Name == name);
		}

		public void AddItem(IInteractableObject item)
		{
			Inventory.Add(item);
		}

		public static ISheet RollNewChar()
		{
			var sheet = new Sheet();

			int skill = StaticRandom.Instance.Next(1, 4) + 7;
			sheet.originalSkill = skill;
			sheet.Skill = skill;

			int stamina = StaticRandom.Instance.Next(2, 13) + 10;
			sheet.originalStamina = stamina;
			sheet.Stamina = stamina;

			int luck = StaticRandom.Instance.Next(1, 7) + 6;
			sheet.originalLuck = luck;
			sheet.Luck = luck;

			sheet.Change = 0;
			sheet.Gold = StaticRandom.Instance.Next(2, 13) + 6;

			sheet.Provisions = 0;

			var sword = new InteractableObject
			{
				Name = "sword",
				Description = "Old sword",
			};

			sword.Interactions.Add(new Interaction
			{
				Action = ActionEnum.Actions.Use,
				ResponseDescription = "You rise your sword...",
				Response = null
			});

			sword.Interactions.Add(new Interaction
			{
				Action = ActionEnum.Actions.Inspect,
				ResponseDescription = "An old and battered sword. Can do some damage, maybe.",
				Response = null
			});

			sheet.AddItem(sword);

			var armour = new InteractableObject
			{
				Name = "armour",
				Description = "Leather armour"
			};

			sheet.AddItem(armour);

			var lantern = new InteractableObject
			{
				Name = "lantern",
				Description = "Lantern"
			};

			sheet.AddItem(lantern);

			var tinderbox = new InteractableObject
			{
				Name = "tinderbox",
				Description = "Tinderbox"
			};

			sheet.AddItem(tinderbox);

			return sheet;
		}

		public string PrintSheet()
		{
			var builder = new StringBuilder();
			builder.AppendLine("HOWL OF THE WEREWOLF ADVENTURE SHEET");
			builder.Append(PrintStats());
			builder.AppendLine(new string('-', 10));
			builder.AppendLine($"GOLD:{Gold}");
			builder.AppendLine($"PROVISIONS:{Provisions}");
			builder.AppendLine($"CHANGE:{Change}");
			builder.AppendLine(new string('-', 10));
            builder.AppendLine("ITEMS AND EQUIPMENT CARRIED");
            builder.Append(PrintInventory());
			return builder.ToString();
		}

		public string PrintInventory()
		{
			var builder = new StringBuilder();

			foreach (var item in Inventory)
			{
				builder.AppendLine("- " + item.Name);
			}

			return builder.ToString();
		}

        public string PrintStats()
        {
            var builder = new StringBuilder();
			builder.AppendLine($"SKILL:{Skill}({originalSkill})");
			builder.AppendLine($"STAMINA:{Stamina}({originalStamina})");
			builder.AppendLine($"LUCK:{Luck}({originalLuck})");
            return builder.ToString();
        }

		public void ReceiveDamage(int damageReceived = 2)
		{
			Stamina -= damageReceived;
		}

		public void ChangeStat(Attributes stat, int value)
		{
			switch(stat)
			{
				case Attributes.Skill:
					Skill += value;
					if(Skill > originalSkill)
						Skill = originalSkill;
					break;
				case Attributes.Stamina:
					Stamina += value;
					if(Stamina > originalStamina)
						Stamina = originalStamina;
					break;
				case Attributes.Luck:
					Luck += value;
					if(Luck > originalLuck)
						Luck = originalLuck;
					break;
				case Attributes.Gold:
					Gold += value;
					break;
				case Attributes.Provision:
					Provisions += value;
					break;
				case Attributes.Change:
					Change += value;
					break;
			}
		}
	}
}