using System;
using System.Collections.Generic;

public delegate string ConsoleCommand(params string[] args);

/// <summary>
/// Use RegisterCommand() to register your own commands.
/// </summary>
public static class ConsoleCommandsDatabase
{
    private static Dictionary<string, ConsoleCommand> _database;
    private static Dictionary<string, ConsoleCommand> database
    {
        get
        {
            if (_database == null)
                _database = new Dictionary<string, ConsoleCommand>(StringComparer.OrdinalIgnoreCase);
            return _database;
        }
    }

    public static void RegisterCommand(string command, ConsoleCommand callback)
    {
        database[command] = new ConsoleCommand(callback);
    }

    public static bool HasCommand(string command)
    {
        return database.ContainsKey(command);
    }

    public static string ExecuteCommand(string command, params string[] args)
    {
        if (HasCommand(command))
        {
            return database[command](args);
        }
        else
        {
            return "Command " + command + " not found.";
        }
    }
}
