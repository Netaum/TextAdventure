using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class ShowInventoryTest
    {
        private ShowInventory action;
        private Mock<IGameController> controllerMock;
        public ShowInventoryTest()
        {
            action = new ShowInventory();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void ShowInventoryShouldCall()
        {
            bool called = false;
            controllerMock.Setup(s => s.Player.InspectInventory())
                          .Callback(() => called = true);
            string[] command = new string[] { "inventory" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.True(called);
        }
    }
}