using System;
using System.IO;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;

namespace TextAdventure.Common.Tools
{
	public static class Tools
	{
		public static ActionEnum? ParseCommand(string actionString)
		{
			if (Enum.TryParse(typeof(ActionEnum), actionString, true, out object actionValue))
			{
				return (ActionEnum)actionValue;
			}
			return null;
		}

		public static T ParseEnum<T>(string enumString) where T : struct, IConvertible
		{
			if (!typeof(T).IsEnum)
			{
				throw new ArgumentException($"{nameof(T)} must be an enumerated type");
			}

			if (Enum.TryParse(typeof(T), enumString, true, out object actionValue))
			{
				return (T)actionValue;
			}
			throw new ArgumentException($"No enumeration found for string {enumString} in {nameof(T)}");

		}

		public static string ReadFile(string file)
		{
			var path = Path.Join(AppDomain.CurrentDomain.BaseDirectory, file);
			using (var reader = new StreamReader(path))
			{
				return reader.ReadToEnd();
			}
		}
	}
}