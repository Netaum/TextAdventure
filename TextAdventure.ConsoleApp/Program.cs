using System;
using System.Threading;
using System.Threading.Tasks;
using TextAdventure.GameEntities.Controllers;
using TextAdventure.GameEntities.Scenes;

namespace TextAdventure.ConsoleApp
{
	class Program
	{
		static void Main(string[] args)
		{
			var scenes = SceneBuilder.LoadScenes();
            var controller = new GameController(scenes);
            controller.Start();
            var t = RunGame(controller);
            t.Wait();
			
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
