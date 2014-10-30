using System.Linq;

namespace UnityConsole.Commands
{
    /// <summary>
    /// A definition for the COMMANDS command.
    /// </summary>
    public static class CommandsCommand
    {
        /// <summary>
        /// Displays the list of available commands.
        /// </summary>
        [Command("COMMANDS", Description = "Displays the list of available commands.", Syntax = "COMMANDS", Override = false)]
        public static string Commands(params string[] args)
        {
            var commands = from commandName in CommandDatabase.commandNames
                           select CommandDatabase.GetCommand(commandName).ToString();
            return "<b>Available commands</b>\n• " + string.Join("\n• ", commands.ToArray());
        }
    }
}