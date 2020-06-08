using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class ShowSheetTest
    {
        private ShowSheet action;
        private Mock<IGameController> controllerMock;
        public ShowSheetTest()
        {
            action = new ShowSheet();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void ShowSheetShouldCall()
        {
            bool called = false;
            controllerMock.Setup(s => s.Player.ShowPlayerSheet())
                          .Callback(() => called = true);
            string[] command = new string[] { "sheet" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.True(called);
        }
    }
}