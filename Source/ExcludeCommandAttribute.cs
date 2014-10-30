using System;

namespace UnityConsole
{
    /// <summary>
    /// Indicates that a command will not be registered with the console automatically at runtime.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExcludeCommandAttribute : Attribute
    {
    }
}