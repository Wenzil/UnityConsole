using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityConsole.Internal;

namespace UnityConsole
{
    /// <summary>
    /// An utility for registering commands with the console. Use ConsoleCommandsDatabase.RegisterCommand() to register commands at runtime.
    /// </summary>
    public static class CommandDatabase
    {
        private static Dictionary<string, Command> database = new Dictionary<string, Command>(StringComparer.OrdinalIgnoreCase);

        /// <summary>
        /// Returns all the command names in alphabetical order.
        /// </summary>
        public static IEnumerable<string> commandNames { get { return database.Keys.OrderBy(name => name); } }

        // Find all static methods decorated with the CommandAttribute attribute and register them
        static CommandDatabase()
        {

            foreach (MethodInfo method in GetAllStaticCommands())
            {
                var excludeCommandAttribute = method.GetCustomAttributes(typeof(ExcludeCommandAttribute), false);
                if (excludeCommandAttribute.Length > 0)
                    continue;
                try
                {
                    CommandAttribute commandAttribute = (CommandAttribute) method.GetCustomAttributes(typeof(CommandAttribute), false)[0];
                    Command.Callback commandCallback = (Command.Callback) Delegate.CreateDelegate(typeof(Command.Callback), method);
                    RegisterCommand(commandAttribute.Name, commandCallback, commandAttribute.Description, commandAttribute.Syntax, commandAttribute.Override);
                }
                catch (ArgumentException)
                {
                    throw new InvalidCommandSignatureException(string.Format("Cannot register command because {0}.{1}() is not compatible with the Command.Callback delegate signature.", method.DeclaringType, method.Name), method);
                }
            }
        }

        // Find all static methods decorated with the CommandAttribute attribute
        private static IEnumerable<MethodInfo> GetAllStaticCommands()
        {
            var bindingFlags = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.DeclaredOnly;
            return RuntimeReflectionUtilities.GetAllMethodsWithAttribute<CommandAttribute>(bindingFlags);
        }

        /// <summary>
        /// Registers a command with the console. If a command with the same name already exists, it is replaced with the new one.
        /// </summary>
        /// <param name="command">The name of the command</param>
        /// <param name="callback">The command execution callback, i.e the method to call when the command is executed</param>
        /// <param name="description">A short description describing what the command does</param>
        /// <param name="syntax">Syntax information for the command arguments</param>
        /// <param name="overrideExisting">Whether to override the command that is already registered with the same name (if there is one)</param>
        public static void RegisterCommand(string command, Command.Callback callback, string description, string syntax)
        {
            RegisterCommand(command, callback, description, syntax, true);
        }

        /// <summary>
        /// Registers a command with the console. If a command with the same name already exists, it is replaced with the new one.
        /// </summary>
        /// <param name="command">The name of the command</param>
        /// <param name="callback">The command execution callback, i.e the method to call when the command is executed</param>
        /// <param name="description">A short description describing what the command does</param>
        /// <param name="syntax">Syntax information for the command arguments</param>
        /// <param name="overrideExisting">Whether to override the command that is already registered with the same name (if there is one)</param>
        public static void RegisterCommand(string command, Command.Callback callback, string description, string syntax, bool overrideExisting)
        {
            command = command.ToUpper();
            description = (string.IsNullOrEmpty(description.Trim()) ? "No description provided" : description);
            syntax = (string.IsNullOrEmpty(syntax.Trim()) ? "No syntax information provided" : syntax);
            
            if (overrideExisting || !HasCommand(command))
                database[command] = new Command(command, callback, description, syntax);
        }

        /// <summary>
        /// Retrieve the given command by name.
        /// </summary>
        /// <returns>The retrieved command</returns>
        /// <exception cref="UnityConsole.NoSuchCommandException">Thrown when there is no such command</exception>
        public static Command GetCommand(string command)
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