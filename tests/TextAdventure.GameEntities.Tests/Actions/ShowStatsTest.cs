using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class ShowStatsTest
    {
        private ShowStats action;
        private Mock<IGameController> controllerMock;
        public ShowStatsTest()
        {
            action = new ShowStats();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void ShowStatsShouldCall()
        {
            bool called = false;
            controllerMock.Setup(s => s.Player.ShowPlayerStats())
                          .Callback(() => called = true);
            string[] command = new string[] { "sheet" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.True(called);
        }
    }
}