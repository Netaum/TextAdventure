using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using TextAdventure.Common.Tools;
using TextAdventure.GameEntities.Controllers;
using TextAdventure.GameEntities.Converters;
using TextAdventure.GameEntities.Items;
using TextAdventure.GameEntities.Scenes;
using TextAdventure.Interfaces;

namespace TextAdventure.ConsoleApp
{
	class Program
	{
		public static void Main2(string[] args)
		{
			var file = Tools.ReadFile("/Files/items.json");
			var settings = new JsonSerializerSettings
			{
				ContractResolver = new DefaultContractResolver(),
			};
			settings.Converters.Add(new InteractableObjectConverter());
			settings.Converters.Add(new InteractionConverter());

			var items = JsonConvert.DeserializeObject<List<InteractableObject>>(file, settings);
		}
		static void Main1(string[] args)
		{
			var scenes = SceneBuilder.LoadScenes();
            var controller = new GameController(scenes);
            controller.Start();
            var t = RunGame(controller);
            t.Wait();
			
		}

		static void Main(string[] args)
		{
			var display = new Controllers.DisplayController();
			var loader = new Controllers.LoadController();

            var controller = new Controllers.GameController(display, loader);
            var t = RunGame2(controller);
            t.Wait();
			
		}

		private static Task RunGame2(TextAdventure.Controllers.GameController controller)
		{
			var cancelationToken = new CancellationTokenSource();
			var t = Task.Run(() =>
			{
				while (true)
				{
					string input = Console.ReadLine().ToLowerInvariant();
					if (input == "exit")
					{
						cancelationToken.Cancel();
						break;
					}

					TextAdventure.Controllers.InputController.ProcessInput(input, controller);
				}
			});

			return t;
		}

		private static Task RunGame(GameController controller)
		{
			var cancelationToken = new CancellationTokenSource();
			var t = Task.Run(() =>
			{
				while (true)
				{
					string input = Console.ReadLine().ToLowerInvariant();
					if (input == "exit")
					{
						cancelationToken.Cancel();
						break;
					}

					InputHandler.ProcessInput(input, controller);
				}
			});

			return t;
		}

	}
}
