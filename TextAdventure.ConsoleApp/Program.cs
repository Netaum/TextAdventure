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
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var display = new Controllers.DisplayController();
			var loader = new Controllers.LoadController(LoadSceneOrigin.Cenarios, args[0]);

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
	}
}
