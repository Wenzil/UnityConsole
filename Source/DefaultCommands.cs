using UnityEngine;
using System.Linq;

namespace UnityConsole
{
    /// <summary>
    /// A handful of built-in console commands. Make sure this script is attached to your Console game object.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("UnityConsole/Default Commands")]
    public class DefaultCommands : MonoBehaviour 
    {
        void Start()
        {
            ConsoleCommandsDatabase.RegisterCommand("COMMANDS", Commands, "Displays the list of available commands.", "COMMANDS");
            ConsoleCommandsDatabase.RegisterCommand("HELP", Help, "Displays general help information, or details about the given command.", "HELP [command]");
            ConsoleCommandsDatabase.RegisterCommand("QUIT", Quit, "Quits the application.", "QUIT");
        }

        /// <summary>
        /// Displays the list of available commands.
        /// </summary>
        public static string Commands(params string[] args)
        {
            var commands = from commandName in ConsoleCommandsDatabase.commandNames
                           select ConsoleCommandsDatabase.GetCommand(commandName).ToString();
            return "<b>Available commands</b>\n• " + string.Join("\n• ", commands.ToArray());
        }

        /// <summary>
        /// Displays general help information, or details about the given command.
        /// </summary>
        public static string Help(params string[] args)
        {
            // if no command argument, display general help information
            if (args.Length < 1)
                return
@"<b>General Help Information</b>
The general syntax for a command is
    COMMAND arg0 arg1 arg2...
Optional arguments are usually placed at the end. To get help about a specific command, type HELP command";

            // otherwise, display details about the given command
            try
            {
                string commandToGetHelpAbout = args[0];
                string details = 
@"<b>Help information about {0}</b>
    Description: {1}
    Syntax: {2}";
                ConsoleCommand retrievedCommand = ConsoleCommandsDatabase.GetCommand(commandToGetHelpAbout);
                return string.Format(details, commandToGetHelpAbout, retrievedCommand.description, retrievedCommand.syntax);
            }
            catch (NoSuchCommandException noSuchCommandException)
            {
                return string.Format("Cannot find help information about {0}. Are you sure it is a valid command?", noSuchCommandException.command);
            }
        }

        /// <summary>
        /// Quits the application.
        /// </summary>
        public static string Quit(params string[] args)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return "Quitting...";
        }
    }
}