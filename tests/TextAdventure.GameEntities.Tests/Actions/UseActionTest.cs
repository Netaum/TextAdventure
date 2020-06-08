using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class UseActionTest
    {
        private UseAction action;
        private Mock<IGameController> controllerMock;
        public UseActionTest()
        {
            action = new UseAction();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void UseActionShouldCall()
        {
            string[] choose = null;
            controllerMock.Setup(s => s.UseItem(It.IsAny<string[]>()))
                          .Callback<string[]>((i) => choose = i);
            string[] command = new string[] { "take", "item" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal(command, choose);
        }
    }
}