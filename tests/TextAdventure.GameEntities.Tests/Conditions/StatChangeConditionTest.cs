using Castle.Components.DictionaryAdapter;
using Moq;
using TextAdventure.GameEntities.Conditions;
using TextAdventure.Interfaces;
using Xunit;
using ICondition = TextAdventure.Interfaces.ICondition;

namespace TextAdventure.GameEntities.Tests.Conditions
{
	public class StatChangeConditionTest
	{
		private Mock<IGameController> controllerMock;

		public StatChangeConditionTest()
		{
			controllerMock = new Mock<IGameController>();
		}

		[Theory]
		[InlineData("skill", "add", 10, 10)]
		[InlineData("skill", "subtract", 10, -10)]
		public void ApplyConditionShouldCall(string attributeString,
											 string checkCondition,
											 int attributeValue,
											 int attributeShould)
		{
			Interfaces.Enums.Stats attribute;
			int statValue = 0;

			controllerMock.Setup(s => s.Player.ChangeStat(It.IsAny<Interfaces.Enums.Stats>(), It.IsAny<int>()))
						  .Callback<Interfaces.Enums.Stats, int>((stat, value) => 
						  {
							  attribute = stat;
							  statValue = value;
						  });
						  
			var c = new StatChangeCondition(attributeString, checkCondition, attributeValue.ToString());
			c.ApplyCondition(controllerMock.Object);
			Assert.Equal(statValue, attributeShould);
		}

		[Fact]
		public void IsConditionFulfilledShouldReturnTrue()
		{
			var c = new StatChangeCondition("skill", "add", "0");
			Assert.True(c.IsConditionFulfilled(null, null));
		}
	}
}