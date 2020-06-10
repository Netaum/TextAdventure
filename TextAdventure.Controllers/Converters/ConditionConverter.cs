using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TextAdventure.Conditions;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Scenes;

namespace TextAdventure.Controllers.Converters
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

				if(type == "inventory")
				{
					return new InventoryCondition((string) obj["checkCondition"],
												  (string) obj["value"]);
				}

				if(type == "damage")
				{
					return new DamageCondition((string) obj["value"],
											   (string) obj["sourceDescription"]);
				}

				if(type == "codeword")
				{
					return new CodeWordCondition((string) obj["value"]);
				}

				if(type == "statChange")
				{
					return new StatChangeCondition((string) obj["attribute"],
												   (string) obj["checkCondition"],
												   (string) obj["value"]);
				}

				if(type == "win")
				{
					return new WinCondition((string) obj["nextScene"]);
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