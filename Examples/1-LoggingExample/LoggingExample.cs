using UnityEngine;
using UnityConsole;

/// <summary>
/// Simple example of how to use the Console.Log() method.
/// </summary>
[AddComponentMenu("UnityConsole/Examples/Logging Example")]
public class LoggingExample : MonoBehaviour
{
    public void LogMessage(string message)
    {
        // Log message to the console. Call this method from anywhere in your code.
        Console.Log(message);
    }
}
	
