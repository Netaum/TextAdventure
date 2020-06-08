using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class CheckActionTest
    {
        private CheckAction action;
        private Mock<IGameController> controllerMock;
        public CheckActionTest()
        {
            action = new CheckAction();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void CheckActionShowEnemyShouldCall()
        {
            int idEnemy = 0;
            controllerMock.Setup(s => s.ShowEnemy(It.IsAny<int>()))
                          .Callback<int>((i) => idEnemy = i);
            string[] command = new string[] { "check", "enemy", "1" };
            action.RespondToInput(controllerMock.Object, command);
            Assert.Equal(1, idEnemy);
        }

        [Fact]
        public void CheckActionShowEnemiesShouldCall()
        {
            bool called = true;
            controllerMock.Setup(s => s.ShowEnemies())
                          .Callback(() => called = true);
            
            string[] command = new string[] { "check", "enemies" };

            action.RespondToInput(controllerMock.Object, command);
            Assert.True(called);
        }
    }
}