using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Scenes;

namespace TextAdventure.Scenes
{
	public class Scene : IScene
	{
		public string Id { get; protected set; }
		public string NextScene { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public IList<IExit> Exits { get; protected set; }
		public IList<IInteractableObject> Objects { get; protected set; }
		public IList<IEnemy> Enemies { get; protected set; }
		public IList<ICondition> Conditions { get; protected set; }
		public IEnemySpawner EnemySpawner { get; protected set; }

		[JsonConstructor]
		public Scene(string id,
						string name,
						string description,
						string nextScene,
						IEnemySpawner enemySpawner)
			: this()
		{
			Description = description;
			Name = name;
			Id = id;
			NextScene = nextScene;
			EnemySpawner = enemySpawner;
		}

		protected Scene()
		{
			Exits = new List<IExit>();
			Objects = new List<IInteractableObject>();
			Enemies = new List<IEnemy>();
			Conditions = new List<ICondition>();
		}

		public void BuildExits(IDictionary<string, IScene> scenes)
		{
			foreach (var exit in Exits)
			{
				exit.FindScene(scenes);
			}
		}
		public string GetExitsDescription() => string.Join("\n", Exits.Select(s => $"- {s.Description} ({s.Key})"));
		public string GetObjectsDescription() => string.Join("\n", Objects.Select(s => s.Description));
		
	}
}