using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class GoActionTest
    {
        private GoAction action;
        private Mock<IGameController> controllerMock;
        public GoActionTest()
        {
            action = new GoAction();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void GoActionShouldCall()
        {
            string choose = string.Empty;
            controllerMock.Setup(s => s.Navigator.AttemptToMove(It.IsAny<string>()))
                          .Callback<string>((i) => choose = i);
            string[] command = new string[] { "move", "test" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal("test", choose);
        }
    }
}