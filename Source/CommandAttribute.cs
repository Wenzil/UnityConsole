using System;

namespace UnityConsole
{
    /// <summary>
    /// Indicates that a method will act as a console command.
    /// </summary>
    /// <remarks>
    /// Apply the [Command] attribute to a method to indicate that the method will act as a console command. The method must be static and compatible with the
    /// <see cref="UnityConsole.Command.Callback"/> delegate signature. When you apply the [Command] attribute to such a method, it will be registered with the console
    /// automatically at runtime.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// The label that refers to the command. Can be used from the console input to execute the command. 
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// A short description describing what the command does.
        /// </summary>
        public string description { get; set; }

        /// <summary>
        /// Syntax information for the command arguments.
        /// </summary>
        public string syntax { get; set; }

        /// <summary>
        /// Whether to override the command that is already registered with the same name (if there is one)
        /// </summary>
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
