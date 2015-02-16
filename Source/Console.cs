using System;
using System.Linq;
using UnityEngine;
using CSharpDocumentation;

[assembly: NamespaceSummary("UnityConsole", "This namespace contains the main UnityConsole components.")]
[assembly: NamespaceSummary("UnityConsole.Commands", "This namespace contains the built-in UnityConsole commands.")]
namespace UnityConsole
{
    [Summary("Static representation of the console. Use Console.Log() anywhere in your code to log messages.")]
    public static class Console
    {
        [Summary("Occurs whenever a new message is logged.")]
        public static event Action<string> onLog;

        [Summary("Occurs whenever the console is cleared.")]
        public static event Action onClear;

        [Summary("Parses the given command input and executes it with the parsed arguments.")]
        [Parameter("input", "The raw command input string for the command (may contain arguments to be parsed)")]
        [Returns("The command response")]
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

        [Summary("Executes the given command with the given command arguments.")]
        [Parameter("command", "The name of the command to execute")]
        [Parameter("args", "The command arguments")]
        [Returns("The command response")]
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

        [Summary("Logs the given message.")]
        public static void Log(string message)
        {
            Debug.Log(message);
            if (onLog != null)
                onLog(message);
        }

        [Summary("Clears the console.")]
        public static void Clear()
        {
            Debug.ClearDeveloperConsole();
            if (onClear != null)
                onClear();
        }
    }
}