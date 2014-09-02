using UnityEngine;
using Wenzil.Console;

/// <summary>
/// A few custom commands being registered with the Console. Only need to do this once per game session.
/// </summary>
public class CustomCommands : MonoBehaviour
{
	void Start()
	{
		ConsoleCommandsDatabase.RegisterCommand("HELP", "Displays help information for a command.", "HELP command", CustomCommands.Help);
		ConsoleCommandsDatabase.RegisterCommand("QUIT", "Quits the game.", "QUIT", CustomCommands.Quit);
		ConsoleCommandsDatabase.RegisterCommand("SPAWN", "Spawns an object.", "SPAWN [name] [PrimitiveType type]", CustomCommands.Spawn);
	}

	/// <summary>
	/// Output a small help message.
	/// </summary>
	private static string Help(CommandInfo info, params string[] args)
	{
		if(args.Length == 0)
			return "USAGE: " + info.usage;

		CommandInfo found;
		if(ConsoleCommandsDatabase.TryGetCommandInfo(args[0], out found))
			return string.Format("{0}\n\r\t{1}", found.description, found.usage);
		else
			return string.Format("COMMAND NOT FOUND: {0}", args[0].ToLower());
	}

	/// <summary>
	/// Quit the application.
	/// </summary>
	private static string Quit(CommandInfo info, params string[] args)
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
	private static string Spawn(CommandInfo info, params string[] args)
	{
		var pt = PrimitiveType.Cube;
		if(args.Length == 2){
			try {
				pt = (PrimitiveType)System.Enum.Parse(typeof(PrimitiveType), args[1], false);
			}
			catch {
				return "INVALID PRIMITIVETYPE SPECIFIED: " + args[1];
			}
		}
		GameObject spawned = GameObject.CreatePrimitive(pt);
		string name = args.Length > 0 ? args[0] : "New GameObject";
		spawned.name = name;
		spawned.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
		return "Spawned a new game object named: " + name;
	}
}