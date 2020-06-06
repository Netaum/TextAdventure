using System;
using Newtonsoft.Json;
using TextAdventure.GameEntities.Items;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Converters
{
    public class InteractableObjectConverter: Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return (typeToConvert == typeof(IInteractableObject));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			return serializer.Deserialize(reader, typeof(InteractableObject));
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(InteractableObject));
		}
	}
}