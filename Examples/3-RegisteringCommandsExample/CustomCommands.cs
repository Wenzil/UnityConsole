using UnityEngine;
using System;
using UnityConsole;

/// <summary>
/// A few custom commands being registered with the console. Simply need to add this script to the Console game object.
/// </summary>
[DisallowMultipleComponent]
public class CustomCommands : MonoBehaviour
{
    void Start()
    {
        // Register a custom command is done by specifying the command name and command execution callback, along with an optional short description and syntax information
        ConsoleCommandsDatabase.RegisterCommand("SPAWN", Spawn, "Spawn a new game object from the given name and primitve type in front of the main camera. See UnityEngine.PrimitiveType.", "SPAWN name primitiveType");
        ConsoleCommandsDatabase.RegisterCommand("KILL", Kill, "Kill the game object with the given name (case sensitive).", "KILL name");
    }

    /// <summary>
    /// Spawn a new game object from the given name and primitve type in front of the main camera.
    /// </summary>
    /// <see cref="UnityEngine.PrimitiveType"/>
    private static string Spawn(params string[] args)
    {
        // If we got the syntax wrong, simply display help information about SPAWN
        if (args.Length < 2)
            return DefaultCommands.Help("SPAWN");

        string name = args[0];
        string primitiveType = args[1];
        PrimitiveType primitiveTypeEnum;
        GameObject gameObject;

        // Parse the given primitive type string into a UnityEngine.PrimitivteType enum
        try
        {
            primitiveTypeEnum = (PrimitiveType)Enum.Parse(typeof(PrimitiveType), primitiveType, true);
        }
        catch
        {
            return "Invalid primitive type specified: " + primitiveType;
        }

        gameObject = GameObject.CreatePrimitive(primitiveTypeEnum);
        gameObject.name = name;
        gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        return "Spawned a new " + primitiveTypeEnum + " named \"" + name + "\"";
    }

    /// <summary>
    /// Kill the game object with the given name (case sensitive).
    /// </summary>
    private static string Kill(params string[] args)
    {
        if (args.Length < 1)
            return DefaultCommands.Help("KILL");

        string name = args[0];
        GameObject gameObject = GameObject.Find(name);

        if (gameObject == null)
            return "No game object named \"" + name + "\" to kill";

        GameObject.Destroy(gameObject);
        return "Killed game object named \"" + name + "\"";
    }
}