using System;
using System.Threading;
using System.Linq;
using System.Text;
using TextAdventure.Interfaces.Scenes;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using System.Collections.Generic;
using TextAdventure.Common.Extensions;

namespace TextAdventure.Controllers
{
	public class DisplayController : IDisplayController
	{
		public void DisplayText(string text,
								int waitMilliseconds = 1)
		{
			var lines = text.Split("\n");
			foreach (var l in lines)
			{
				Console.WriteLine(l);
				Thread.Sleep(waitMilliseconds);
			}
		}

		public void DisplayEnemyDescription(IEnemy enemy)
		{
            var builder = new StringBuilder();
			builder.AppendLine($"NAME: {enemy.Name}");
			builder.AppendLine($"SKILL: {enemy.Skill}");
			builder.AppendLine($"STAMINA: {enemy.Stamina}");
            DisplayText(builder.ToString());
		}

		public void DisplayEnemyList(IEnumerable<IEnemy> enemies)
		{
            var display = GetEnemiesString(enemies);
			DisplayText(display);
		}

		private string GetEnemiesInScene(IScene scene)
		{
			if(!scene.Enemies.Any())
				return null;
			return GetEnemiesString(scene.Enemies);
		}

		private string GetEnemiesString(IEnumerable<IEnemy> enemies)
		{
			var builder = new StringBuilder();
			string title = $"------  ENEMIES ------";
			builder.AppendLine(title);
			foreach(var (enemy, index) in enemies.WithIndex())
			{
				builder.AppendLine($"{index + 1} - {enemy.Name} ({enemy.EnemyType})");
			}
			return builder.ToString();
		}

		public void DisplaySceneDescription(IScene scene)
		{
			var objectDescription = GetObjectsDescriptionInScene(scene);
			var exitDescription = GetExitsDescriptionInScene(scene);
			var enemyDescription = GetEnemiesInScene(scene);

			var builder = new StringBuilder();

			builder.AppendLine(scene.Description);
			
			if (!string.IsNullOrEmpty(objectDescription))
				builder.AppendLine(objectDescription);

			if (!string.IsNullOrEmpty(exitDescription))
				builder.AppendLine(exitDescription);

			if (!string.IsNullOrEmpty(enemyDescription))
				builder.AppendLine(enemyDescription);

			DisplayText(builder.ToString());
		}

		public void DisplayPlayerInventory(IPlayer player)
		{
			var builder = new StringBuilder();
			builder.AppendLine("You look in your backpack, inside you have: ");
			foreach (var item in player.Inventory)
			{
				builder.AppendLine("- " + item.Name);
			}
			builder.AppendLine(new string('-', 10));
			builder.AppendLine($"Gold - {player.Gold}");
			builder.AppendLine($"Provisions - {player.Provisions}");

			DisplayText(builder.ToString());
		}

		public void DisplayPlayerEquipment(IPlayer player)
		{
			var builder = new StringBuilder();
			builder.AppendLine("You have the following itens equiped: ");
			foreach(var equip in player.Equipment)
			{
				builder.AppendLine($"- {equip.Value.Name} ({equip.Key})");
			}
			DisplayText(builder.ToString());
		}

		public void DisplayPlayerStats(IPlayer player)
		{
			var builder = new StringBuilder();
			builder.AppendLine($"Skill - {player.Skill}");
			builder.AppendLine($"Stamina - {player.Stamina}");
			builder.AppendLine($"Luck - {player.Luck}");
			builder.AppendLine($"Change - {player.Change}");
            DisplayText(builder.ToString());
		}
		private string GetObjectsDescriptionInScene(IScene scene)
		{
			return string.Join("\n", scene.Objects.Select(s => s.Description));
		}
		private string GetExitsDescriptionInScene(IScene scene)
		{
			return string.Join("\n", scene.Exits.Select(s => $"- {s.Description} ({s.Key})"));
		}
	}
}