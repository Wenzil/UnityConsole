using System;
using CSharpDocumentation;

namespace UnityConsole
{
    [Summary("Indicates that a method will act as a console command. The method must be static and compatible with the *Command.Callback* delegate signature. When you apply this attribute to such a method, it will be registered with the console automatically at runtime.")]
    [SeeAlso("[Command.Callback](#unityconsole-namespace-commandcallback)")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        [Summary("The label that refers to the command. Can be used from the console input to execute the command. ")]
        public string name { get; private set; }

        [Summary("A short description describing what the command does.")]
        public string description { get; set; }

        [Summary("Syntax information for the command arguments.")]
        public string syntax { get; set; }

        [Summary("Whether to override the command that is already registered with the same name (if there is one)")]
        public bool overrideRegistered { get; set; }

        public CommandAttribute(string name)
        {
            this.name = name;
            description = "No description provided";
            syntax = "No syntax information provided";
            overrideRegistered = true;
        }
    }
}
