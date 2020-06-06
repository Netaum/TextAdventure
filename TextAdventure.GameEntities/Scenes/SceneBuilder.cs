using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TextAdventure.Common.Extensions;
using TextAdventure.Common.Tools;
using TextAdventure.GameEntities.Converters;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Scenes
{
	public class SceneBuilder
	{
		public static IDictionary<string, IScene> LoadScenes()
		{
			//var file = Tools.ReadFile("/Scenes/howl_werewolf.json");
			var file = Tools.ReadFile("/Scenes/Scenes.json");
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver(),
			};

			settings.Converters.Add(new ExitConverter());
			settings.Converters.Add(new SceneConverter());
			settings.Converters.Add(new InteractableObjectConverter());
			settings.Converters.Add(new InteractionConverter());
			settings.Converters.Add(new EnemyConverter());
			settings.Converters.Add(new ConditionConverter());

			var scenes = JsonConvert.DeserializeObject<Dictionary<string, IScene>>(file, settings);
			scenes.BuildExits();
			return scenes;
		}
	}
}