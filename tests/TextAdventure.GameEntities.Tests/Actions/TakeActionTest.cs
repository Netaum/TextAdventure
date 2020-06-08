using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class TakeActionTest
    {
        private TakeAction action;
        private Mock<IGameController> controllerMock;
        public TakeActionTest()
        {
            action = new TakeAction();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void TakeActionShouldCall()
        {
            string[] choose = null;
            controllerMock.Setup(s => s.TakeItem(It.IsAny<string[]>()))
                          .Callback<string[]>((i) => choose = i);
            string[] command = new string[] { "take", "item" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal(command, choose);
        }
    }
}