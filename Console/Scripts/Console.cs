using System;
using UnityEngine;

public delegate void OnConsoleLog(string line);

/// <summary>
/// Use Console.Log() anywhere in your code. The Console prefab will display the output.
/// </summary>
public static class Console
{
    public static OnConsoleLog OnConsoleLog;

    public static void Log(string line)
    {
        Debug.Log(line);
        if (OnConsoleLog != null)
            OnConsoleLog(line);
    }

    public static string ExecuteCommand(string command, params string[] args)
    {
        return ConsoleCommandsDatabase.ExecuteCommand(command, args);
    }
}
