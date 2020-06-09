using Moq;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Scenes;
using Xunit;

namespace TextAdventure.Controllers.Tests
{
    public class GameControllerTest
    {
        private IGameController gameController;
        private Mock<IDisplayController> mockDisplay;
        private Mock<ILoadController> mockLoader;

        public GameControllerTest()
        {
            mockDisplay = new Mock<IDisplayController>();
            mockLoader = new Mock<ILoadController>();
            gameController = new GameController(mockDisplay.Object, mockLoader.Object);
        }

        [Fact]
        public void MovePlayer_ShouldMove()
        {
            string sceneName = "Scene 01";
            string sceneDescription = "Scene 01 description";
            string consoleDescription = string.Empty;
            IScene displayedScene = null;

            var scene = new Mock<IScene>();
            scene.Setup(s => s.Description)
                 .Returns(sceneDescription);

            scene.Setup(s => s.Name)
                 .Returns(sceneName);

            mockLoader.Setup(s => s.LoadScene(It.IsAny<string>()))
                      .Returns(scene.Object);

            mockDisplay.Setup(s => s.DisplayText(It.IsAny<string>(), It.IsAny<int>()))
                       .Callback<string, int>((desc, milli) => consoleDescription = desc);

            mockDisplay.Setup(s => s.DisplaySceneDescription(It.IsAny<IScene>()))
                       .Callback<IScene>((s) => displayedScene = s);

            gameController.MovePlayer(sceneName, null, null);

            Assert.Equal($"You moved to {sceneName}", consoleDescription);
            Assert.Equal(scene.Object, gameController.CurrentScene);
            Assert.Equal(scene.Object, displayedScene);
        }
    }
}