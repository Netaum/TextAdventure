using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
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

				return jsonSettings;
			}
		}
		public IScene LoadScene(string sceneName)
		{
			var fileName = $"/Scenes/{sceneName}.json";
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