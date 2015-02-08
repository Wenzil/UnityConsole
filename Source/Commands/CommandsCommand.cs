using System.Linq;
using CSharpDocumentation;

namespace UnityConsole.Commands
{
    [Summary("A definition for the COMMANDS command.")]
    public static class CommandsCommand
    {
        [Summary("Displays the list of available commands.")]
        [Command("COMMANDS", description = "Displays the list of available commands.", syntax = "COMMANDS", overrideRegistered = false)]
        public static string Commands(params string[] args)
        {
            var commands = from commandName in CommandDatabase.commandNames
                           select CommandDatabase.GetCommand(commandName).ToString();
            return "<b>Available commands</b>\n• " + string.Join("\n• ", commands.ToArray());
        }
    }
}