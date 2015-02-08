using CSharpDocumentation;

namespace UnityConsole.Commands
{
    [Summary("A definition for the HELP command.")]
    public static class HelpCommand
    {
        [Summary("Displays general syntax information or specific command usage.")]
        [Command("HELP", description = "Displays general syntax information or specific command usage.", syntax = "HELP [command]", overrideRegistered = false)]
        public static string Help(params string[] args)
        {
            // if no command argument, display general help information
            if (args.Length < 1)
                return
@"<b>General Syntax Information</b>
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
                Command retrievedCommand = CommandDatabase.GetCommand(commandToGetHelpAbout);
                return string.Format(details, commandToGetHelpAbout, retrievedCommand.description, retrievedCommand.syntax);
            }
            catch (NoSuchCommandException noSuchCommandException)
            {
                return string.Format("Cannot find help information about {0}. Are you sure it is a valid command?", noSuchCommandException.command);
            }
        }
    }
}