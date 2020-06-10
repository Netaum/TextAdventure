using TextAdventure.Interfaces.Controllers;

namespace TextAdventure.Controllers
{
	public static class InputController
	{
		private static char[] delimiters = new char[] { ' ' };
		public static void ProcessInput(string input,
										IGameController controller)
		{
			var cleanInput = input.ToLowerInvariant();
			var commands = cleanInput.Split(delimiters);

			var command = controller.GetInputCommand(commands[0]);
			command?.RespondToInput(controller, commands);
		}

	}
}