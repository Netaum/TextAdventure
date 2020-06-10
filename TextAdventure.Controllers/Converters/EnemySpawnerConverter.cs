using System;
using Newtonsoft.Json;
using TextAdventure.Interfaces.Scenes;
using TextAdventure.Scenes;

namespace TextAdventure.Controllers.Converters
{
	public class EnemySpawnerConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type objectType)
		{
			return (objectType == typeof(IEnemySpawner));
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize(reader, typeof(EnemySpawner));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			
		}
	}
}