using Moq;
using TextAdventure.GameEntities.Actions;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.GameEntities.Tests.Actions
{
    public class AttackActionTest
    {
        private AttackAction action;
        private Mock<IGameController> controllerMock;
        public AttackActionTest()
        {
            action = new AttackAction();
            controllerMock = new Mock<IGameController>();
        }

        [Fact]
        public void AttackActionShouldCall()
        {
            bool called = true;
            controllerMock.Setup(s => s.AttackEnemy())
                          .Callback(() => called = true);
            
            action.RespondToInput(controllerMock.Object, null);
            Assert.True(called);
        }
    }
}