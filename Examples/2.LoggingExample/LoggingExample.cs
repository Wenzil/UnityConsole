using UnityEngine;
using UnityConsole;

namespace UnityConsole.Examples
{
    // Simple example of how to use the Console.Log() and Console.Clear() methods.
    [AddComponentMenu("UnityConsole/Examples/Logging Example")]
    public class LoggingExample : MonoBehaviour
    {
        public void LogMessage()
        {
            // Log message to the console. Call this method from anywhere in your code.
            Console.Log("Logged a message because Console.Log() was called");
        }

        public void ClearConsole()
        {
            // Clear the console. Call this method from anywhere in your code.
            Console.Clear();
        }
    }
}