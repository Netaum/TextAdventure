using Moq;
using TextAdventure.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Entities;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class ChooseCommandTest
    {
        private ChooseCommand action;
        private Mock<IGameController> controllerMock;
        private Mock<IPlayer> playerMock;
        public ChooseCommandTest()
        {
            action = new ChooseCommand();
            controllerMock = new Mock<IGameController>();
            playerMock = new Mock<IPlayer>();
            
        }

        [Fact]
        public void ChooseActionShouldCall()
        {
            string choose = string.Empty;
            controllerMock.Setup(s => s.RespondCommandChoice(It.IsAny<string>()))
                          .Callback<string>((i) => choose = i);
            string[] command = new string[] { "choose", "test" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal("test", choose);
        }

    }
}