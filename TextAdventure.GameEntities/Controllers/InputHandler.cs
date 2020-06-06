using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Controllers
{
    public static class InputHandler
    {
        private static char[] delimiters = new char[] { ' ' };
        public static void ProcessInput(string input,
                                        IGameController controller)
        {
            var cleanInput = input.ToLowerInvariant();
            var commands = cleanInput.Split(delimiters);

            var action = controller.GetAction(commands[0]);
            action?.RespondToInput(controller, commands);
        }
    }
    
}