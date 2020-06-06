using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using TextAdventure.Common.Tools;
using TextAdventure.Interfaces;
using ActionEnum = TextAdventure.Interfaces.Enums.Actions;

namespace TextAdventure.GameEntities.Scenes
{
	public class Scene : IScene
	{
		public string Id { get; protected set; }
		public string NextScene { get; protected set; }
		public string Name { get; protected set; }
		public string Description { get; protected set; }
		public string ShortDescription { get; protected set; }
		public IList<IExit> Exits { get; protected set; }
		public IList<IInteractableObject> Objects { get; protected set; }
		public IList<IEnemy> Enemies { get; protected set; }
		public IList<ICondition> Conditions { get; protected set; }
		public bool FileDescription { get; protected set; } = false;

		[JsonConstructor]
		protected Scene(string id,
						string nextScene,
						string name,
						string description,
						string shortDescription,
						bool fileDescription)
		{
			Exits = new List<IExit>();
			Objects = new List<IInteractableObject>();
			Enemies = new List<IEnemy>();
			Conditions = new List<ICondition>();

			Description = description;
			ShortDescription = shortDescription;
			Name = name;
			Id = id;
			FileDescription = fileDescription;
			NextScene = nextScene;
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
		public string GetSceneDescription()
		{
			if(FileDescription)
			{
				Description = Tools.ReadFile($"/Scenes/Descriptions/{Id}.txt");
			}
			return Description;
		}
		public IResponseAction TryUseItem(string item, IGameController controller)
		{
			var actionItem = Objects.FirstOrDefault(f => f.Name == item);

			if (actionItem == null)
				return null;

			var action = actionItem.GetInteraction(ActionEnum.Use);

			if (action?.Response == null)
				return null;

			controller.DisplayText(action.ResponseDescription);

			return action.Response;
		}

		public IInteractableObject GetItem(string item)
		{
			var actionItem = Objects.First(f => f.Name == item);
			Objects.Remove(actionItem);
			return actionItem;
		}

		public bool ItemExists(string item)
		{
			return Objects.Any(a => a.Name == item);
		}
	}
}