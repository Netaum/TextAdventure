using System.Collections.Generic;
using System.Linq;
using Moq;
using TextAdventure.Interfaces;
using Xunit;

namespace TextAdventure.Entities.Tests
{
	public class NavigatorTest
	{
		private Navigator navigator;
		private Mock<IGameController> controllerMock;
		private Mock<IScene> sceneMock;

		public NavigatorTest()
		{
			controllerMock = new Mock<IGameController>();
			sceneMock = new Mock<IScene>();
			navigator = new Navigator(controllerMock.Object);
		}

        [Fact]
        public void SetNextScene_ShouldSet()
        {
            navigator.SetNextScene(sceneMock.Object);
            Assert.Equal(sceneMock.Object, navigator.CurrentScene);
        }

        [Fact]
        public void UnpackScene_ShouldUpack()
        {
            var exit1 = new Mock<IExit>();
            exit1.Setup(s => s.Key)
                 .Returns("Exit 01");

            var exit2 = new Mock<IExit>();
            exit2.Setup(s => s.Key)
                 .Returns("Exit 02");
            
            sceneMock.Setup(s => s.Exits)
                     .Returns(new List<IExit>{exit1.Object, exit2.Object});

            navigator.SetNextScene(sceneMock.Object);
            navigator.UnpackScene();

            Assert.Equal(2, navigator.SceneExits.Count());
        }

        [Fact]
        public void SetNextScene_WithExits_ShouldSet()
        {
              var exit1 = new Mock<IExit>();
            exit1.Setup(s => s.Key)
                 .Returns("Exit 01");

            var exit2 = new Mock<IExit>();
            exit2.Setup(s => s.Key)
                 .Returns("Exit 02");
            
            sceneMock.Setup(s => s.Exits)
                     .Returns(new List<IExit>{exit1.Object, exit2.Object});

            navigator.SetNextScene(sceneMock.Object);
            navigator.UnpackScene();

            Assert.Equal(2, navigator.SceneExits.Count());

            navigator.SetNextScene(sceneMock.Object);
            Assert.Equal(0, navigator.SceneExits.Count());
        }
	}
}