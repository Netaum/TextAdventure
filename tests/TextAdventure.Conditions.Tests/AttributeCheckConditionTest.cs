using Moq;
using Xunit;
using TextAdventure.Conditions;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Conditions;
using TextAdventure.Interfaces.Entities;

namespace TextAdventure.GameEntities.Tests.Conditions
{
	public class AttributeCheckConditionTest
	{
		private ICondition condition;
		private Mock<IGameController> controllerMock;

		public AttributeCheckConditionTest()
		{
			controllerMock = new Mock<IGameController>();
			condition = new AttributeCheckCondition("skill", "less", "3", "500");
		}

		[Fact]
		public void ApplyConditionShouldCall()
		{
			string nextScene = string.Empty;
			controllerMock.Setup(s => s.MovePlayer(It.IsAny<string>(), null, string.Empty))
						  .Callback<string, string, string>((dest, origin, desc) => nextScene = dest);

			condition.ApplyCondition(controllerMock.Object);
			Assert.Equal("500", nextScene);
		}

		[Theory]
		[InlineData("skill", "5", 4, true)]
		[InlineData("stamina", "2", 1, true)]
		[InlineData("combatTurn", "7", 6, true)]
        [InlineData("skill", "5", 5, false)]
		[InlineData("stamina", "2", 2, false)]
		[InlineData("combatTurn", "7", 7, false)]
        [InlineData("skill", "5", 6, false)]
		[InlineData("stamina", "2", 3, false)]
		[InlineData("combatTurn", "7", 8, false)]
		public void IsConditionFulfilledLessShouldBeEqualAssert(string stat,
                                                                string value,
                                                                int enemyValue,
                                                                bool assertValue)
		{
			var c = new AttributeCheckCondition(stat, "less", value, string.Empty);
			var e = new Mock<IEnemy>();

			e.Setup(s => s.Skill)
			 .Returns(enemyValue);

			e.Setup(s => s.CombatTurn)
			 .Returns(enemyValue);

			e.Setup(s => s.Stamina)
			 .Returns(enemyValue);

			var result = c.IsConditionFulfilled(null, e.Object);

			Assert.Equal(assertValue, result);

		}

        [Theory]
		[InlineData("skill", "5", 4, true)]
		[InlineData("stamina", "2", 1, true)]
		[InlineData("combatTurn", "7", 6, true)]
        [InlineData("skill", "5", 5, true)]
		[InlineData("stamina", "2", 2, true)]
		[InlineData("combatTurn", "7", 7, true)]
        [InlineData("skill", "5", 6, false)]
		[InlineData("stamina", "2", 3, false)]
		[InlineData("combatTurn", "7", 8, false)]
		public void IsConditionFulfilledLessOrEqualShouldBeEqualAssert(string stat,
                                                                string value,
                                                                int enemyValue,
                                                                bool assertValue)
		{
			var c = new AttributeCheckCondition(stat, "lessOrEqual", value, string.Empty);
			var e = new Mock<IEnemy>();

			e.Setup(s => s.Skill)
			 .Returns(enemyValue);

			e.Setup(s => s.CombatTurn)
			 .Returns(enemyValue);

			e.Setup(s => s.Stamina)
			 .Returns(enemyValue);

			var result = c.IsConditionFulfilled(null, e.Object);

			Assert.Equal(assertValue, result);

		}

        [Theory]
		[InlineData("skill", "5", 4, false)]
		[InlineData("stamina", "2", 1, false)]
		[InlineData("combatTurn", "7", 6, false)]
        [InlineData("skill", "5", 5, true)]
		[InlineData("stamina", "2", 2, true)]
		[InlineData("combatTurn", "7", 7, true)]
        [InlineData("skill", "5", 6, false)]
		[InlineData("stamina", "2", 3, false)]
		[InlineData("combatTurn", "7", 8, false)]
		public void IsConditionFulfilledEqualShouldBeEqualAssert(string stat,
                                                                string value,
                                                                int enemyValue,
                                                                bool assertValue)
		{
			var c = new AttributeCheckCondition(stat, "equal", value, string.Empty);
			var e = new Mock<IEnemy>();

			e.Setup(s => s.Skill)
			 .Returns(enemyValue);

			e.Setup(s => s.CombatTurn)
			 .Returns(enemyValue);

			e.Setup(s => s.Stamina)
			 .Returns(enemyValue);

			var result = c.IsConditionFulfilled(null, e.Object);

			Assert.Equal(assertValue, result);

		}

        [Theory]
		[InlineData("skill", "5", 4, false)]
		[InlineData("stamina", "2", 1, false)]
		[InlineData("combatTurn", "7", 6, false)]
        [InlineData("skill", "5", 5, true)]
		[InlineData("stamina", "2", 2, true)]
		[InlineData("combatTurn", "7", 7, true)]
        [InlineData("skill", "5", 6, true)]
		[InlineData("stamina", "2", 3, true)]
		[InlineData("combatTurn", "7", 8, true)]
		public void IsConditionFulfilledGreaterEqualShouldBeEqualAssert(string stat,
                                                                string value,
                                                                int enemyValue,
                                                                bool assertValue)
		{
			var c = new AttributeCheckCondition(stat, "greaterOrEqual", value, string.Empty);
			var e = new Mock<IEnemy>();

			e.Setup(s => s.Skill)
			 .Returns(enemyValue);

			e.Setup(s => s.CombatTurn)
			 .Returns(enemyValue);

			e.Setup(s => s.Stamina)
			 .Returns(enemyValue);

			var result = c.IsConditionFulfilled(null, e.Object);

			Assert.Equal(assertValue, result);

		}

        [Theory]
		[InlineData("skill", "5", 4, false)]
		[InlineData("stamina", "2", 1, false)]
		[InlineData("combatTurn", "7", 6, false)]
        [InlineData("skill", "5", 5, false)]
		[InlineData("stamina", "2", 2, false)]
		[InlineData("combatTurn", "7", 7, false)]
        [InlineData("skill", "5", 6, true)]
		[InlineData("stamina", "2", 3, true)]
		[InlineData("combatTurn", "7", 8, true)]
		public void IsConditionFulfilledGreaterShouldBeEqualAssert(string stat,
                                                                string value,
                                                                int enemyValue,
                                                                bool assertValue)
		{
			var c = new AttributeCheckCondition(stat, "greater", value, string.Empty);
			var e = new Mock<IEnemy>();

			e.Setup(s => s.Skill)
			 .Returns(enemyValue);

			e.Setup(s => s.CombatTurn)
			 .Returns(enemyValue);

			e.Setup(s => s.Stamina)
			 .Returns(enemyValue);

			var result = c.IsConditionFulfilled(null, e.Object);

			Assert.Equal(assertValue, result);

		}
	}
}