using System;
using Newtonsoft.Json;
using TextAdventure.GameEntities.Conditions;
using TextAdventure.GameEntities.Scenes;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Converters
{
	public class EnemyConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return (typeToConvert == typeof(IEnemy));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize(reader, typeof(Enemy));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(Enemy));
		}
	}
}