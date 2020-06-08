using Castle.Components.DictionaryAdapter;
using Moq;
using TextAdventure.GameEntities.Conditions;
using TextAdventure.Interfaces;
using Xunit;
using ICondition = TextAdventure.Interfaces.ICondition;

namespace TextAdventure.GameEntities.Tests.Conditions
{
	public class InventoryConditionTest
	{
		private Mock<IGameController> controllerMock;

		public InventoryConditionTest()
		{
			controllerMock = new Mock<IGameController>();
		}

		[Fact]
		public void ApplyConditionAddShouldCall()
		{
			bool called = false;
			controllerMock.Setup(s => s.Player.AddItemToInventory(It.IsAny<string>()))
						  .Callback(() => called = true);
			var c = new InventoryCondition("add", string.Empty);
			c.ApplyCondition(controllerMock.Object);
			Assert.True(called);
		}

		[Fact]
		public void ApplyConditionSubstractShouldCall()
		{
			bool called = false;
			controllerMock.Setup(s => s.Player.RemoveItemFromInventory(It.IsAny<string>()))
						  .Callback(() => called = true);
			var c = new InventoryCondition("subtract", string.Empty);
			c.ApplyCondition(controllerMock.Object);
			Assert.True(called);
		}

		[Fact]
		public void IsConditionFulfilledAddShouldReturnTrue()
		{
			var c = new InventoryCondition("add", string.Empty);
			Assert.True(c.IsConditionFulfilled(null, null));
		}

		[Fact]
		public void IsConditionFulfilledRemoveHasItemShouldReturnTrue()
		{
			var c = new InventoryCondition("Subtract", "item");
			controllerMock.Setup(s => s.Player.HasItem(It.IsAny<string>()))
						  .Returns(true);
			Assert.True(c.IsConditionFulfilled(controllerMock.Object, null));
		}

		[Fact]
		public void IsConditionFulfilledRemoveHasItemShouldReturnFalse()
		{
			var c = new InventoryCondition("Subtract", "item");
			controllerMock.Setup(s => s.Player.HasItem(It.IsAny<string>()))
						  .Returns(false);
			Assert.False(c.IsConditionFulfilled(controllerMock.Object, null));
		}
	}
}