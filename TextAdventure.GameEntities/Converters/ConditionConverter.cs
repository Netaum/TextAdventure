using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TextAdventure.GameEntities.Conditions;
using TextAdventure.GameEntities.Scenes;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Converters
{
	public class ConditionConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return (typeToConvert == typeof(ICondition));
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			else
			{
				JObject obj = JObject.Load(reader);
				var type = obj["type"].ToString();

				if(type == "attributeCheck")
				{
					return new AttributeCheckCondition((string) obj["attribute"],
													   (string) obj["checkCondition"],
													   (string) obj["value"],
													   (string) obj["nextScene"]);
				}
				if(type == "damage")
				{
					return new DamageCondition((string) obj["value"],
											   (string) obj["sourceDescription"]);
				}
				if(type == "statChange")
				{
					return new StatChangeCondition((string) obj["attribute"],
												   (string) obj["checkCondition"],
												   (string) obj["value"]);
				}
				
				return null;
			}
		}

		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			serializer.Serialize(writer, value, typeof(Exit));
		}
	}
}