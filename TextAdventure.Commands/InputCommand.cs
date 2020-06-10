using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TextAdventure.Interfaces.Commands;
using TextAdventure.Interfaces.Controllers;
using TextAdventure.Interfaces.Enums;

namespace TextAdventure.Commands
{
	public abstract class InputCommand : IInputCommand
	{
		public InputCommand(PlayerCommands command)
		{
			Command = command;
		}
		public PlayerCommands Command { get; private set; }
		public abstract void RespondToInput(IGameController controller, string[] commands);

		public static IList<IInputCommand> GetAllCommands()
		{
			var list = new List<IInputCommand>();
			var commandNamespace = "TextAdventure.Commands";
			var types = Assembly.GetExecutingAssembly()
								.GetTypes()
								.Where(w => w.IsClass && 
											!w.IsAbstract &&
											w.Namespace == commandNamespace)
								.ToList();

			foreach(var type in types)
			{
				if(type.DeclaringType != null && type.DeclaringType.IsAbstract)
					continue;
				
				var command = (IInputCommand) Activator.CreateInstance(type);
				list.Add(command);
			}
			return list;
		}
	}
}