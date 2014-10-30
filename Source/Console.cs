using System;
using System.Linq;
using UnityEngine;

namespace UnityConsole
{
    /// <summary>
    /// Static representation of the console. Use Console.Log() anywhere in your code to log messages.
    /// </summary>
    public static class Console
    {
        /// <summary>
        /// Occurs whenever a new message is logged.
        /// </summary>
        public static event Action<string> onLog;

        /// <summary>
        /// Occurs whenever the console is cleared.
        /// </summary>
        public static event Action onClear;

        /// <summary>
        /// Parses the given command input and executes it with the parsed arguments.
        /// </summary>
        /// <param name="input">The raw command input string for the command (may contain arguments to be parsed)</param>
        /// <returns>The command response</returns>
        public static string ExecuteCommand(string input)
        {
            string[] parts = input.Split(' ');
            string command = parts[0];
            string[] args = parts.Skip(1).ToArray();
            string response = TryExecuteCommand(command, args);

            Console.Log("> " + input);
            Console.Log(response);
            return response;
        }

        /// <summary>
        /// Executes the given command with the given command arguments.
        /// </summary>
        /// <param name="command">The name of the command to execute</param>
        /// <param name="args">The command arguments</param>
        /// <returns>The command response</returns>
        public static string ExecuteCommand(string command, params string[] args)
        {
            string response = TryExecuteCommand(command, args);

            Console.Log("> " + command + string.Join(" ", args));
            Console.Log(response);
            return response;
        }

        private static string TryExecuteCommand(string command, params string[] args)
        {
            try
            {
                Command toExecute = CommandDatabase.GetCommand(command);
                return toExecute.callback(args);
            }
            catch (NoSuchCommandException noSuchCommandException)
            {
                return noSuchCommandException.Message;
            }
        }

        /// <summary>
        /// Logs the given message.
        /// </summary>
        public static void Log(string message)
        {
            Debug.Log(message);
            if (onLog != null)
                onLog(message);
        }

        /// <summary>
        /// Clears the console.
        /// </summary>
        public static void Clear()
        {
            Debug.ClearDeveloperConsole();
            if (onClear != null)
                onClear();
        }
    }
}