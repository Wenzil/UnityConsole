using System;
using CSharpDocumentation;

namespace UnityConsole
{
    [Summary("Indicates that a command will not be registered with the console automatically at runtime.")]
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
    public class ExcludeCommandAttribute : Attribute
    {
    }
}