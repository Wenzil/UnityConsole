using UnityEngine;
using System;
using Wenzil.Console;

/// <summary>
/// A few custom commands being registered with the Console. Only need to do this once per game session.
/// </summary>
public class CustomCommands : MonoBehaviour
{
	void Start()
	{
		
		ConsoleCommandsDatabase.RegisterCommand("QUIT", "Quits the application.", "QUIT", CustomCommands.Quit);
        ConsoleCommandsDatabase.RegisterCommand("SPAWN", "Spawn a new game object from the given name and primitve type in front of the main camera. See PrimitiveType.", "SPAWN [name] [primitiveType]", CustomCommands.Spawn);
        ConsoleCommandsDatabase.RegisterCommand("HELP", "Displays help information for the given command.", "HELP [command]", CustomCommands.Help);
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
    /// Spawn a new game object from the given name and primitve type in front of the main camera. See PrimitiveType.
	/// </summary>
	private static string Spawn(params string[] args)
	{
        string name = "New GameObject";
        PrimitiveType primitiveType = PrimitiveType.Cube;
        GameObject spawned;
        
        if(args.Length >= 2)
        {
            name = args[0];
			try 
            {
				primitiveType = (PrimitiveType) Enum.Parse(typeof(PrimitiveType), args[1], true);
			}
			catch 
            {
				return "Invalid primitive type specified: " + args[1];
			}
		}

		spawned = GameObject.CreatePrimitive(primitiveType);
		spawned.name = name;
		spawned.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
		return "Spawned a new " + primitiveType + " named: " + name;
	}

    /// <summary>
    /// (Advanced) Displays help information for the given command.
    /// </summary>
    private static string Help(params string[] args)
	{
        // if we got it wrong, get help about the HELP command
        if (args.Length == 0)
            return Help("HELP");

        // if we got it right, get help about the given command
        string commandToGetHelpAbout = args[0];
		ConsoleCommand found;
		if(ConsoleCommandsDatabase.TryGetCommand(commandToGetHelpAbout, out found))
			return string.Format("{0}\n\r\t{1}", found.description, found.usage);
		else
			return string.Format("Command not found: {0}", commandToGetHelpAbout.ToUpper());
	}
}