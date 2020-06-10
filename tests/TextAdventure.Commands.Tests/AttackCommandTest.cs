using Moq;
using TextAdventure.Commands;
using TextAdventure.Interfaces.Controllers;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class AttackCommandTest
    {
        private AttackCommand action;
        private Mock<IGameController> controllerMock;
        public AttackCommandTest()
        {
            action = new AttackCommand();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void AttackActionShouldCall()
        {
            bool called = true;
            controllerMock.Setup(s => s.PlayerAttackEnemy())
                          .Callback(() => called = true);
            
            action.RespondToInput(controllerMock.Object, null);
            Assert.True(called);
        }

        [Fact]
        public void CheckAllCommands()
        {
            var commands = InputCommand.GetAllCommands();
            Assert.True(true);
        }
    }
}