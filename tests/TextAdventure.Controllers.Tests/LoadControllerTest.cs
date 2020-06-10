using Moq;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Scenes;
using Xunit;

namespace TextAdventure.Controllers.Tests
{
    public class LoadControllerTest
    {
        private LoadController controller;

        public LoadControllerTest()
        {
            controller = new LoadController();
        }

        [Fact]
        public void LoadSimpleScene_ShouldWork()
        {
            var testFile = "test_scene01";
            var scene = controller.LoadScene(testFile);
            Assert.NotNull(scene);
            Assert.NotNull(scene.Name);
            Assert.NotNull(scene.Description);
            Assert.NotNull(scene.NextScene);
        }

        [Fact]
        public void LoadScene_WithExits_ShouldWork()
        {
            var testFile = "test_scene02";
            var scene = controller.LoadScene(testFile);
            Assert.NotNull(scene);
            Assert.NotNull(scene.Name);
            Assert.NotNull(scene.Description);
            Assert.Null(scene.NextScene);
            Assert.NotEmpty(scene.Exits);
        }

        [Fact]
        public void LoadScene_WithConditions_ShouldWork()
        {
            var testFile = "test_scene05";
            var scene = controller.LoadScene(testFile);
            Assert.NotNull(scene);
            Assert.NotNull(scene.Name);
            Assert.NotNull(scene.Description);
            Assert.NotNull(scene.NextScene);
            Assert.Empty(scene.Exits);
            Assert.NotEmpty(scene.Conditions);
            Assert.Collection(scene.Conditions, cond => Assert.Equal("InventoryCondition", cond.Type),
                                                cond => Assert.Equal("CodeWordCondition", cond.Type));
        } 

        [Fact]
        public void LoadScene_WithSimpleEnemy_ShouldWork()
        {
            var testFile = "test_scene03";
            var scene = controller.LoadScene(testFile);
            Assert.NotNull(scene);
            Assert.NotNull(scene.Name);
            Assert.NotNull(scene.Description);
            Assert.Null(scene.NextScene);
            Assert.Empty(scene.Exits);
            Assert.NotEmpty(scene.Enemies);
            Assert.NotNull(scene.Enemies[0].Name);
        }

        [Fact]
        public void LoadScene_WithEnemy_WithCombatConditions_ShouldWork()
        {
            var testFile = "test_scene04";
            var scene = controller.LoadScene(testFile);
            Assert.NotNull(scene);
            Assert.NotNull(scene.Name);
            Assert.NotNull(scene.Description);
            Assert.Null(scene.NextScene);
            Assert.Empty(scene.Exits);
            Assert.NotEmpty(scene.Enemies);
            Assert.NotNull(scene.Enemies[0].Name);
            Assert.NotEmpty(scene.Enemies[0].CombatConditions);
        }

        [Fact]
        public void LoadScene_EnemySpawner_ShouldWork()
        {
            var testFile = "test_scene06";
            var scene = controller.LoadScene(testFile);
            Assert.NotNull(scene);
            Assert.NotNull(scene.Name);
            Assert.NotNull(scene.Description);
            Assert.Null(scene.NextScene);
            Assert.NotNull(scene.EnemySpawner);
        }
    }
}