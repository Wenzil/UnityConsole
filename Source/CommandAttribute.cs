using System;

namespace UnityConsole
{
    /// <summary>
    /// Indicates that a method will act as a console command.
    /// </summary>
    /// <remarks>
    /// Apply the CommandAttribute attribute to a method to indicate that the method will act as a console command. The method must be static and compatible with the
    /// <see cref="UnityConsole.Command.Callback"/> delegate signature. When you apply the CommandAttribute attribute to such a method, it will be registered with the console
    /// automatically at runtime.
    /// </remarks>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class CommandAttribute : Attribute
    {
        /// <summary>
        /// The label that refers to the command. Can be used from the console input to execute the command. 
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// A short description describing what the command does.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Syntax information for the command arguments.
        /// </summary>
        public string Syntax { get; set; }

        /// <summary>
        /// Whether to override the command that is already registered with the same name (if there is one)
        /// </summary>
        public bool Override { get; set; }

        public CommandAttribute(string name)
        {
            Name = name;
            Description = "No description provided";
            Syntax = "No syntax information provided";
            Override = true;
        }
    }
}
