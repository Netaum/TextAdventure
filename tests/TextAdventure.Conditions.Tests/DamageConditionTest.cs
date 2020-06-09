using Moq;
using Xunit;
using TextAdventure.Conditions;
using TextAdventure.Interfaces.Controllers;

namespace TextAdventure.GameEntities.Tests.Conditions
{
	public class DamageConditionTest
	{
		private Mock<IGameController> controllerMock;

		public DamageConditionTest()
		{
			controllerMock = new Mock<IGameController>();
		}

		[Theory]
		[InlineData("10", "Ten damage")]
		[InlineData("100", "")]
		[InlineData("50", null)]
		[InlineData("3", "Three damage done.")]
		public void ApplyConditionShouldCall(string damage, string source)
		{
			string damageReceived = string.Empty;
			string description = string.Empty;

			controllerMock.Setup(s => s.DoDamageToPlayer(It.IsAny<int>(), It.IsAny<string>()))
						  .Callback<int, string>((d, desc) => 
						  {
							  damageReceived = d.ToString();
							  description = desc;
						  });
						  
			var c = new DamageCondition(damage, source);
			c.ApplyCondition(controllerMock.Object);
			Assert.Equal(damageReceived, damage);
			Assert.Equal(description, source);
		}

		[Fact]
		public void IsConditionFulfilledShouldReturnTrue()
		{
			var c = new DamageCondition(string.Empty, string.Empty);
			Assert.True(c.IsConditionFulfilled(null, null));
		}
	}
}