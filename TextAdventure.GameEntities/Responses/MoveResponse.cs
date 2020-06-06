using TextAdventure.Interfaces;

namespace TextAdventure.GameEntities.Responses
{
    public class MoveResponse: ResponseAction, IResponseAction
    {
        private string destinationScene;
        private string currentScene;
        private string description;
        public MoveResponse(string destinationScene,
                            string description,
                            string currentScene = null)
        {
            this.destinationScene = destinationScene;
            this.currentScene = currentScene;
            this.description = description;
        }

        public override bool DoResponseAction(IGameController controller)
		{
			return controller.MovePlayer(destinationScene, currentScene, description);
		}
    }
}