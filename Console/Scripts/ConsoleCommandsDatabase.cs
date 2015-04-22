using System;
using System.Collections.Generic;
using System.Linq;

namespace Wenzil.Console
{
    /// <summary>
    /// Use RegisterCommand() to register your own commands.
    /// </summary>
    public static class ConsoleCommandsDatabase 
    {
        private static Dictionary<string, ConsoleCommand> database = new Dictionary<string, ConsoleCommand>(StringComparer.OrdinalIgnoreCase);
        
        public static void RegisterCommand(string command, ConsoleCommandCallback callback) 
        {
            RegisterCommand(command, "", "", callback);
        }

        public static void RegisterCommand(string command, string description, string usage, ConsoleCommandCallback callback)
        {
            database.Add(command, new ConsoleCommand(description, usage, callback));
        }

        public static string ExecuteCommand(string command, params string[] args)
        {
            ConsoleCommand found;
            if (TryGetCommand(command, out found))
                return found.callback(args);
            else
                return "Command " + command.ToUpper() + " not found.";
        }

        public static bool TryGetCommand(string command, out ConsoleCommand result)
        {
            if(HasCommand(command))
            {
                result = database[command];
                return true;
            }
            else
            {
                result = default(ConsoleCommand);
                return false;
            }
        }

        public static ConsoleCommand GetCommand(string command)
        {
            return database[command];
        }

        public static bool HasCommand(string command)
        {
            return database.ContainsKey(command);
        }
    }
}