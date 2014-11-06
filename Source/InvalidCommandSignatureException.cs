using System;
using System.Reflection;
using System.Runtime.Serialization;

namespace UnityConsole
{
    /// <summary>
    /// An exception thrown when a static command has an invalid method signature
    /// </summary>
    [Serializable]
    public class InvalidCommandSignatureException : Exception
    {
        /// <summary>
        /// The command with the invalid method signature.
        /// </summary>
        public MethodInfo command { get; private set; }

        public InvalidCommandSignatureException() : base() { }

        public InvalidCommandSignatureException(string message) : base(message) { }

        public InvalidCommandSignatureException(string message, MethodInfo command) : base(message)
        {
            this.command = command;
        }

        protected InvalidCommandSignatureException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            if (info != null)
                this.command = (MethodInfo) info.GetValue("command", typeof(MethodInfo));
        }

        /// <summary>
        /// Perform serialization. Not part of the public API.
        /// </summary>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (info != null)
                info.AddValue("command", command, typeof(MethodInfo));
        }
    }
}
