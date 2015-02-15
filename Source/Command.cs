using CSharpDocumentation;
using System;

namespace UnityConsole 
{
    [Summary("Command information and execution callback.")]
    public struct Command 
    {
        [Summary("The command execution callback signature.")]
        [Parameter("args", "The command arguments")]
        [Returns("The command response")]
        public delegate string Callback(params string[] args);

        [Summary("The command identifier used to access and execute the command.")]
        public string name { get; private set; }

        [Summary("The command execution callback, i.e. the method to call when the command is executed.")]
        public Command.Callback callback { get; private set; }

        [Summary("A short description describing what the command does.")]
        public string description { get; private set; }

        [Summary("Syntax information for the command arguments.")]
        public string syntax { get; private set; }

        [Summary("Constructs a Command with the given name, execution callback, description and syntax information.")]
        [CodeExample("var command = new Command(\"join\", args => string.Join(\", \", args), \"Join the string arguments together.\", \"join args\");")]
        public Command(string name, Command.Callback callback, string description, string syntax) : this()
        {
            this.name = name;
            this.callback = callback;
            this.description = description;
            this.syntax = syntax;
        }

        [Summary("Returns a string representation of the command in the format: name - description")]
        public override string ToString()
        {
            return name + " - " + description;
        }
    }
}