namespace UnityConsole 
{
    /// <summary>
    /// Command information and execution callback.
    /// </summary>
    public struct Command 
    {
        /// <summary>
        /// The command execution callback signature.
        /// </summary>
        /// <param name="args">The command arguments</param>
        /// <returns>The command response</returns>
        public delegate string Callback(params string[] args);

        /// <summary>
        /// The label that refers to the command. Can be used from the console input to execute the command. 
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// The command execution callback, i.e. the method to call when the command is executed.
        /// </summary>
        public Command.Callback callback { get; private set; }

        /// <summary>
        /// A short description describing what the command does.
        /// </summary>
        public string description { get; private set; }

        /// <summary>
        /// Syntax information for the command arguments.
        /// </summary>
        public string syntax { get; private set; }

        /// <summary>
        /// Constructs a ConsoleCommand with the given name, execution callback, description and syntax information.
        /// </summary>
        public Command(string name, Command.Callback callback, string description, string syntax) : this()
        {
            this.name = name;
            this.callback = callback;
            this.description = description;
            this.syntax = syntax;
        }

        public override string ToString()
        {
            return name + " - " + description;
        }
    }
}