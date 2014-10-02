namespace UnityConsole 
{
    /// <summary>
    /// The execution callback of a command.
    /// </summary>
    /// <param name="args">The command arguments</param>
    /// <returns>The command response</returns>
    public delegate string ConsoleCommandCallback(params string[] args);

    /// <summary>
    /// Command information and execution callback.
    /// </summary>
	public struct ConsoleCommand 
    {
        /// <summary>
        /// The name of a command with which you can execute the command from the console input. 
        /// </summary>
        public string name { get; private set; }

        /// <summary>
        /// The command execution callback, i.e. the method to call when the command is executed.
        /// </summary>
        public ConsoleCommandCallback callback { get; private set; }

        /// <summary>
        /// A short description describing what the command does.
        /// </summary>
		public string description { get; private set; }

        /// <summary>
        /// Syntax information for the command arguments (if any). The general syntax for a Command is
        ///     Command arg0 arg1 ... argN
        /// Arguments (if any) are separated by whitespace. Optional arguments are usually placed at the end.
        /// </summary>
		public string syntax { get; private set; }

        /// <summary>
        /// Constructs a ConsoleCommand with the given name, execution callback, description and syntax information.
        /// </summary>
        public ConsoleCommand(string name, ConsoleCommandCallback callback, string description, string syntax)
        {
            this.name = name;
            this.callback = callback;
			this.description = description;
			this.syntax = syntax;
		}
	}
}