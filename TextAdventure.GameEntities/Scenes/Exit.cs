using System.Collections.Generic;
using Newtonsoft.Json;
using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Scenes
{
	public class Exit : IExit
	{
		public int Id { get; private set; }
		public string SceneId { get; private set; }
		public string Description { get; private set; }
		public string Key { get; private set; }
		public IScene Scene { get; private set; }

		[JsonConstructor]
		public Exit(int id,
					string sceneId,
					string description,
					string key)
		{
			Id = id;
			SceneId = sceneId;
			Key = key;
			Description = description;
		}

		public void FindScene(IDictionary<string, IScene> scenes)
		{
			if (!scenes.ContainsKey(SceneId))
			{
				return;
				//throw new System.Exception($"Scene {SceneId} not found in scenes");
			}

			Scene = scenes[SceneId];
		}
	}
}