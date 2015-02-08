using System;
using System.Runtime.Serialization;
using CSharpDocumentation;

namespace UnityConsole
{
    [Summary("An exception thrown when attempting to retrieve a command that does not exist.")]
    [Serializable]
    public class NoSuchCommandException : Exception
    {
        [Summary("The command that does not exist.")]
        public string command { get; private set; }

        public NoSuchCommandException() : base() { }

        public NoSuchCommandException(string message) : base(message) { }

        public NoSuchCommandException(string message, string command) : base(message)
        {
            this.command = command;
        }

        protected NoSuchCommandException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
                this.command = info.GetString("command");
        }

        [Summary("Perform serialization. Not part of the public API.")]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
                info.AddValue("command", command);
        }
    }
}
