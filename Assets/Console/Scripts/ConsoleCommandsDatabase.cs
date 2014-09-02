using System;
using System.Collections.Generic;
using System.Linq;
namespace Wenzil.Console
{

	public delegate string ConsoleCommand(CommandInfo info, params string[] args);

	/// <summary>
	/// Use RegisterCommand() to register your own commands.
	/// </summary>
	public static class ConsoleCommandsDatabase 
	{
		private static Dictionary<string, CommandInfo> database = new Dictionary<string, CommandInfo>(StringComparer.OrdinalIgnoreCase);

		public static void RegisterCommand(string command, string description, string usage, ConsoleCommand callback)
		{
			database.Add(command, new CommandInfo(description, usage, callback));
		}

		public static bool HasCommand(string command)
		{
			return database.ContainsKey(command);
		}

		public static bool TryGetCommandInfo(string command, out CommandInfo result) {
			if(HasCommand(command)){
				result = database[command];
				return true;
			}
			else {
				result = default(CommandInfo);
				return false;
			}
		}

		public static CommandInfo GetCommandInfo(string command) {
			return database[command];
		}

		public static string ExecuteCommand(string command, params string[] args)
		{
			if(HasCommand(command))
				return database[command].callback(database[command], args);
			else
				return "Command " + command + " not found.";
		}
	}
}