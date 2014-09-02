using UnityEngine;

/// <summary>
/// A few custom commands being registered with the Console. Only need to do this once per game session.
/// </summary>
public class CustomCommands : MonoBehaviour
{
    void Start()
    {
        ConsoleCommandsDatabase.RegisterCommand("HELP", CustomCommands.Help);
        ConsoleCommandsDatabase.RegisterCommand("QUIT", CustomCommands.Quit);
        ConsoleCommandsDatabase.RegisterCommand("SPAWN", CustomCommands.Spawn);
    }

    /// <summary>
    /// Output a small help message.
    /// </summary>
    private static string Help(params string[] args)
    {
        if (args.Length == 0)
            return Help("null", "null");
        else if (args.Length == 1)
            return Help(args[0], "null");
        else
            return "This is your help! arg0 = " + args[0] + " and arg1 = " + args[1];
    }

    /// <summary>
    /// Quit the application.
    /// </summary>
    private static string Quit(params string[] args)
    {
        Application.Quit();
        
#if UNITY_EDITOR
        if (Application.isEditor)
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        return "Quitting...";
    }

    /// <summary>
    /// Spawn a new primitive game object with the given name in front of the main camera.
    /// </summary>
    private static string Spawn(params string[] args)
    {
        GameObject spawned = GameObject.CreatePrimitive(PrimitiveType.Cube);
        string name = args.Length > 0 ? args[0] : "New GameObject";
        spawned.name = name;
        spawned.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
        return "Spawned a new game object named: " + name;
    }
}
