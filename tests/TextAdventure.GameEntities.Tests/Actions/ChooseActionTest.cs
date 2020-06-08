using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class ChooseActionTest
    {
        private ChooseAction action;
        private Mock<IGameController> controllerMock;
        private Mock<IPlayer> playerMock;
        public ChooseActionTest()
        {
            action = new ChooseAction();
            controllerMock = new Mock<IGameController>();
            playerMock = new Mock<IPlayer>();
            
        }

        [Fact]
        public void ChooseActionShouldCall()
        {
            string choose = string.Empty;
            controllerMock.Setup(s => s.Navigator.AttempChoose(It.IsAny<string>()))
                          .Callback<string>((i) => choose = i);
            string[] command = new string[] { "choose", "test" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal("test", choose);
        }

    }
}