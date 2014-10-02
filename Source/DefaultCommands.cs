using UnityEngine;
using System.Linq;

namespace UnityConsole
{
    /// <summary>
    /// A handful of built-in console commands. To use them, make sure this script is attached to your Console game object.
    /// </summary>
    [DisallowMultipleComponent]
    public class DefaultCommands : MonoBehaviour 
    {
        void Start()
        {
            ConsoleCommandsDatabase.RegisterCommand("COMMANDS", Commands, "Displays a list of all available commands.", "COMMANDS");
            ConsoleCommandsDatabase.RegisterCommand("HELP", Help, "Displays general help information or details about the given command.", "HELP [command]");
            ConsoleCommandsDatabase.RegisterCommand("QUIT", Quit, "Quits the application.", "QUIT");
        }

        /// <summary>
        /// Displays a list of all available commands
        /// </summary>
        public static string Commands(params string[] args)
        {
            return "<b>Command List</b>\n- " + string.Join("\n- ", ConsoleCommandsDatabase.commandNames);
        }

        /// <summary>
        /// Displays general help information or details about the given command."
        /// </summary>
        public static string Help(params string[] args)
        {
            // if no command argument, display general help information
            if (args.Length < 1)
                return "<b>General Help Information</b>\nExecuting commands is simple. The general syntax for a Command is \n\tCommand arg0 arg1 ... argN \nArguments (if any) are separated by whitespace. Optional arguments are usually placed at the end.\n<b>Helpful Tips</b>\n- Type COMMANDS to list all available commands. To learn more about a specific Command, type HELP Command. \n- Use the Up/Down arrow keys to navigate your command history.";

            // otherwise, display details about the given command
            try
            {
                string commandToGetHelpAbout = args[0];
                ConsoleCommand retrievedCommand = ConsoleCommandsDatabase.GetCommand(commandToGetHelpAbout);
                return string.Format("<b>Help information about {0}</b>\n\r\tDescription: {1}\n\r\tSyntax: {2}", commandToGetHelpAbout, retrievedCommand.description, retrievedCommand.syntax);
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