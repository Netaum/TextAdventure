using System;
using Newtonsoft.Json;
using TextAdventure.Interfaces.Scenes;
using TextAdventure.Scenes;

namespace TextAdventure.Controllers.Converters
{
    public class SceneConverter: Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return (typeToConvert == typeof(IScene));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize(reader, typeof(Scene));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(Scene));
		}
	}
}