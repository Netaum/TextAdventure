using Moq;
using TextAdventure.Commands;
using TextAdventure.Interfaces.Controllers;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class CheckCommandTest
    {
        private CheckCommand action;
        private Mock<IGameController> controllerMock;
        public CheckCommandTest()
        {
            action = new CheckCommand();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void CheckActionShowEnemyShouldCall()
        {
            int? idEnemy = 0;
            controllerMock.Setup(s => s.DisplayEnemyInformation(It.IsAny<int?>()))
                          .Callback<int?>((i) => idEnemy = i);
            string[] command = new string[] { "check", "enemy", "1" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal(1, idEnemy.Value);
        }

        [Fact]
        public void CheckActionShowEnemiesShouldCall()
        {
            bool called = true;
            controllerMock.Setup(s => s.DisplayEnemyInformation(null))
                          .Callback(() => called = true);
            
            string[] command = new string[] { "check", "enemies" };

            action.RespondToInput(controllerMock.Object, command);
            Assert.True(called);
        }
    }
}