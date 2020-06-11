using System;
using System.IO;
using TextAdventure.Interfaces.Enums;
using Xunit;
using ActionEnum = TextAdventure.Interfaces.Enums.PlayerCommands;
using ParseTool = TextAdventure.Common.Tools;

namespace TextAdventure.Common.Tests.Tools
{
	public class ToolTestFixture : IDisposable
	{
		const string fileName = "unitTest.txt";
        public const string fileContet = "This is the content";

		public ToolTestFixture()
		{
            File.WriteAllText(GetPath(), fileContet);
		}

		public void Dispose()
		{
			File.Delete(GetPath());
		}

        public string GetPath()
        {
            return Path.Join(AppDomain.CurrentDomain.BaseDirectory, "tests", fileName);
        }
	}
	public class ToolsTest
	{
		[Theory]
		[InlineData("attack", ActionEnum.Attack)]
		[InlineData("check", ActionEnum.Check)]
		[InlineData("choose", ActionEnum.Choose)]
		[InlineData("go", ActionEnum.Go)]
		[InlineData("inspect", ActionEnum.Inspect)]
		[InlineData("inventory", ActionEnum.Inventory)]
		[InlineData("sheet", ActionEnum.Sheet)]
		[InlineData("stats", ActionEnum.Stats)]
		[InlineData("take", ActionEnum.Take)]
		[InlineData("use", ActionEnum.Use)]
		public void ParseActionShouldWork(string action, ActionEnum actionEnumExpected)
		{
			var result = ParseTool.Tools.ParseCommand(action);
			Assert.Equal(result, actionEnumExpected);
		}

		[Theory]
		[InlineData("")]
		[InlineData("blabla")]
		public void ParseActionShouldNotWork(string action)
		{
			var result = ParseTool.Tools.ParseCommand(action);
			Assert.Null(result);
		}

		[Theory]
		[InlineData("change", Stats.Change)]
		[InlineData("code", Stats.Code)]
		[InlineData("combatTurn", Stats.CombatTurn)]
		[InlineData("gold", Stats.Gold)]
		[InlineData("item", Stats.Item)]
		[InlineData("luck", Stats.Luck)]
		[InlineData("provision", Stats.Provision)]
		[InlineData("skill", Stats.Skill)]
		[InlineData("stamina", Stats.Stamina)]
		public void ParseAttributeShouldWork(string attribute, Stats attributeExpected)
		{
			var result = ParseTool.Tools.ParseEnum<Stats>(attribute);
			Assert.Equal(result, attributeExpected);
		}

		[Theory]
		[InlineData("")]
		[InlineData("blabla")]
		public void ParseAttributeShouldThrowException(string attribute)
		{
			Assert.Throws<ArgumentException>(() => ParseTool.Tools.ParseEnum<Stats>(attribute));
		}

		[Theory]
		[InlineData("add", CheckCondition.Add)]
		[InlineData("addP", CheckCondition.Increase)]
		[InlineData("equal", CheckCondition.Equal)]
		[InlineData("greater", CheckCondition.Greater)]
		[InlineData("greaterOrEqual", CheckCondition.GreaterOrEqual)]
		[InlineData("less", CheckCondition.Less)]
		[InlineData("lessOrEqual", CheckCondition.LessOrEqual)]
		[InlineData("subtract", CheckCondition.Subtract)]
		[InlineData("subtractP", CheckCondition.Decrease)]
		public void ParseCheckConditionShouldWork(string attribute, CheckCondition checkExpected)
		{
			var result = ParseTool.Tools.ParseEnum<CheckCondition>(attribute);
			Assert.Equal(result, checkExpected);
		}

		[Theory]
		[InlineData("")]
		[InlineData("blabla")]
		public void ParseCheckConditionShouldThrowException(string attribute)
		{
			Assert.Throws<ArgumentException>(() => ParseTool.Tools.ParseEnum<CheckCondition>(attribute));
		}
	}
}