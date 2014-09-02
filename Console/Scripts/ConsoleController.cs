using UnityEngine;
using System.Linq;

/// <summary>
/// The behavior of the Console.
/// </summary>
public class ConsoleController : MonoBehaviour 
{
    public ConsoleUI ui;
    public KeyCode toggleKey = KeyCode.Tab;

    void OnEnable()
    {
        Console.OnConsoleLog += ui.AddNewOutputLine;
        ui.onSubmitCommand += ExecuteCommand;
    }

    void OnDisable()
    {
        Console.OnConsoleLog -= ui.AddNewOutputLine;
        ui.onSubmitCommand -= ExecuteCommand;
    }

    void Update()
    {
        if (Input.GetKeyDown(toggleKey))
            ui.ToggleConsole();
        else if (Input.GetKeyDown(KeyCode.Escape))
            ui.CloseConsole();
    }

    private void ExecuteCommand(string input)
    {
        string[] parts = input.Split(' ');
        string command = parts[0];
        string[] args = parts.Skip(1).ToArray();
        
        Console.Log("> " + input);
        Console.Log(ConsoleCommandsDatabase.ExecuteCommand(command, args));
    }
}
