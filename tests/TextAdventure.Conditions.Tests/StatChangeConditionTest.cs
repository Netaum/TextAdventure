using Moq;
using Xunit;
using TextAdventure.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Entities;
using TextAdventure.Interfaces.Enums;

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
		[InlineData("skill", "subtract", 10, 10)]
		public void ApplyConditionShouldCall(string attributeString,
											 string checkCondition,
											 int attributeValue,
											 int attributeShould)
		{
			Stats attribute;
			CheckCondition condition;
			int statValue = 0;
			controllerMock.Setup(s => s.ChangePlayerStat(It.IsAny<Stats>(), It.IsAny<CheckCondition>(), It.IsAny<int>()))
						  .Callback<Stats, CheckCondition, int>((stat, cond, value) => 
						  {
							  attribute = stat;
							  statValue = value;
							  condition = cond;
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