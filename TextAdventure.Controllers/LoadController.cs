using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TextAdventure.Controllers.Converters;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Scenes;

namespace TextAdventure.Controllers
{
	public class LoadController : ILoadController
	{
		private static JsonSerializerSettings jsonSettings;
		private static object syncLock = new object();
		private static JsonSerializerSettings Instance
		{
			get
			{
				if (jsonSettings != null)
					return jsonSettings;

				lock (syncLock)
				{
					if (jsonSettings != null)
						return jsonSettings;

					jsonSettings = new JsonSerializerSettings
					{
						ContractResolver = new DefaultContractResolver(),
					};
				}

				jsonSettings.Converters.Add(new ExitConverter());
				jsonSettings.Converters.Add(new SceneConverter());
				jsonSettings.Converters.Add(new InteractableObjectConverter());
				jsonSettings.Converters.Add(new InteractionConverter());
				jsonSettings.Converters.Add(new EnemyConverter());
				jsonSettings.Converters.Add(new ConditionConverter());

				return jsonSettings;
			}
		}
		public IScene LoadScene(string sceneName)
		{
			var fileName = $"/Scenes/json/{sceneName}.json";
			var file = TextAdventure.Common.Tools.Tools.ReadFile(fileName);
			return JsonConvert.DeserializeObject<IScene>(file, Instance);
		}

		public IList<IInteractableObject> LoadGameItens()
		{
			var fileName = $"/Itens/items.json";
			var file = TextAdventure.Common.Tools.Tools.ReadFile(fileName);
			return JsonConvert.DeserializeObject<IList<IInteractableObject>>(file, Instance);
		}
	}
}