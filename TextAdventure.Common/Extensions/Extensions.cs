using System.Collections.Generic;
using TextAdventure.Interfaces;
using System.Linq;

namespace TextAdventure.Common.Extensions
{
    public static class Extensions
	{
		public static void BuildExits(this IDictionary<string, IScene> scenes)
		{
			foreach(var scene in scenes)
			{
				foreach(var exit in scene.Value.Exits)
				{
					exit.FindScene(scenes);
				}
			}
		}

		public static IEnumerable<(T item, int index)> WithIndex<T>(this IEnumerable<T> list) => 
			list.Select((item, index) => (item, index));
	}
}