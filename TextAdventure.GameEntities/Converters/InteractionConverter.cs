using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TextAdventure.Common.Tools;
using TextAdventure.GameEntities.Actions;
using TextAdventure.GameEntities.Items;
using TextAdventure.GameEntities.Responses;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Converters
{
	public class InteractionConverter : Newtonsoft.Json.JsonConverter
	{
		public override bool CanConvert(Type typeToConvert)
		{
			return (typeToConvert == typeof(IInteraction));
		}
		public override object ReadJson(JsonReader reader,
										Type objectType,
										object existingValue,
										JsonSerializer serializer)
		{
			if (reader.TokenType == JsonToken.Null)
			{
				return null;
			}
			else
			{
				JObject obj = JObject.Load(reader);
				var action = Tools.ParseAction(obj["action"].ToString());

				var responseInteraction = new Interaction();
				if(action.HasValue)
					responseInteraction.Action = action.Value;

				responseInteraction.ResponseDescription = obj["responseDescription"].ToString();

				if (obj["response"] != null)
				{
					var response = obj["response"];
					switch(response["response"].ToString())
					{
						case "MoveResponse":
							string destinationScene = (string) response["destinationScene"];
							string currentScene = (string) response["currentScene"];
							string description = response["description"]?.ToString();
							responseInteraction.Response = new MoveResponse(destinationScene, description, currentScene);
							break;
					}
				}

				return responseInteraction;
			}
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			throw new NotImplementedException();
		}
	}
}