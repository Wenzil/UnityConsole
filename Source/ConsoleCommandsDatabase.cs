using System;
using System.Collections.Generic;
using System.Linq;

namespace UnityConsole
{
	/// <summary>
    /// An utility for registering custom commands with the console. Use ConsoleCommandsDatabase.RegisterCommand() to register your own commands.
	/// </summary>
	public static class ConsoleCommandsDatabase 
	{
		private static Dictionary<string, ConsoleCommand> database = new Dictionary<string, ConsoleCommand>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Returns all the command names in alphabetical order.
        /// </summary>
        public static string[] commandNames
        {
            get
            {
                return database.Keys.OrderBy(name => name).ToArray();
            }
        }
		
        /// <summary>
        /// Registers a command with the console. If a command with the same name already exists, it is replaced with the new one.
        /// </summary>
        /// <param name="command">The name of the command</param>
        /// <param name="callback">The command execution callback, i.e the method to call when the command is executed</param>
		public static void RegisterCommand(string command, ConsoleCommandCallback callback) 
        {
            RegisterCommand(command, callback, "", "");
		}

        /// <summary>
        /// Registers a command with the console. If a command with the same name already exists, it is replaced with the new one.
        /// </summary>
        /// <param name="command">The name of the command</param>
        /// <param name="callback">The command execution callback, i.e the method to call when the command is executed</param>
        /// <param name="description">A short description describing what the command does</param>
        /// <param name="syntax">Syntax information for the command arguments (if any)</param>
        public static void RegisterCommand(string command, ConsoleCommandCallback callback, string description, string syntax)
        {
            command = command.ToUpper();
            description = (string.IsNullOrEmpty(description.Trim()) ? "No description provided" : description);
            syntax = (string.IsNullOrEmpty(syntax.Trim()) ? "No syntax information provided" : syntax);
            database[command] = new ConsoleCommand(command, callback, description, syntax);
        }

        /// <summary>
        /// Retrieve the given command by name.
        /// </summary>
        /// <returns>The retrieved command</returns>
        /// <exception cref="UnityConsole.NoSuchCommandException">Thrown when there is no such command</exception>
        public static ConsoleCommand GetCommand(string command)
        {
            if (HasCommand(command))
                return database[command];
            else
                throw new NoSuchCommandException("Command " + command.ToUpper() + " not found.", command);
        }

        /// <summary>
        /// Returns whether a command with the given name exists.
        /// </summary>
        public static bool HasCommand(string command)
        {
            return database.ContainsKey(command);
        }
	}
}