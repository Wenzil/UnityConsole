UnityConsole
===============
Welcome to UnityConsole, an easy-to-use developer console for Unity 4.6!

![Screenshot](https://dl.dropboxusercontent.com/u/106740647/UnityConsole/ScreenshotCropped.jpg)

## Installation
Clone this repository and put the UnityConsole directory into your Assets folder.

Alternatively, download and import one of the following unity packages:
- [UnityConsoleWithExampleScenes](http://wenzil.github.io/UnityConsole/) which includes useful step-by-step learning examples
- [UnityConsoleVRDemoAlpha](http://wenzil.github.io/UnityConsole/)  which includes a basic demo for early stage VR support
- [UnityConsoleMinimal](http://wenzil.github.io/UnityConsole/) which includes nothing but the necessary UnityConsole components

## Getting Started
1. Add a UI canvas to your scene if you don't already have one (GameObject > UI > Canvas)
2. Drag-and-Drop the UnityConsole prefab onto the Canvas in the Hierarchy
3. Run your scene and press TAB to toggle the console

## Logging
Anywhere in your code, simply use ```Console.Log()``` to output to the console.

## Executing Commands
To execute a command, simply type its name into the console input, followed by whitespace-separated arguments if any. 

UnityConsole comes with 3 built-in commands.
- `commands` - Display the list of all available commands
- `help` - Display general help information, or details about the given command
- `quit` - Quit the application

For example, typing ```help quit``` will execute the `help` command with *quit* as the first and only argument. So the output will display details (such as a short description and usage syntax) about the `quit` command.

## Defining Custom Commands
UnityConsole allows you to write your own commands using C#. The process is done in two easy steps.

1. Define a method compatible with the [Command.Callback](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-command-callback) delegate signature, i.e. a method that takes a string array parameter and returns a string. The string array parameter contains any command-line arguments passed in. The return value is the command output.
2. Register the command with the console, specifying the command name, description and syntax. The description and syntax are for reference only.

### Non-Static Commands
Commands defined in non-static methods must be registered manually by invoking the ```CommandDatabase.RegisterCommand()``` method at runtime. It can be done at any point, but a good place for it is within the Start() method of a script.

For example, the following script defines a non-static command `toggle_ui` whose job is to toggle the UI visibility of the game.
```csharp
using UnityConsole;

public class NonStaticCommandExample : MonoBehaviour
{
    // Store a reference to the UI Canvas object in the scene (assigned from the inspector)
    public Canvas UI;

    // Manually register our non-static command whenever this object is initialized
    void Start()
    {
        CommandDatabase.RegisterCommand("toggle_ui", ToggleUI, "Toggles the UI visibility", "toggle_ui");
    }

    // Define the command
    public string ToggleUI(params string[] args)
    {
        UI.enabled = !UI.enabled;
        return "UI visibility turned " + (UI.enabled ? "on" : "off");
    }
}
```

### Static Commands
Commands defined in static methods can be registered by simply applying the [CommandAttribute](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-commandattribute) attribute to the method. 

For example, the following class defines a static command `hello` whose job is to output "Hello World!". Since the command is static and is decorated with the [CommandAttribute](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-commandattribute) attribute, it will be registered with the console automatically at runtime.
```csharp
using UnityConsole;

public class StaticCommandExample
{
    [Command("hello", description = "Outputs \"Hello World!\"", syntax = "hello")]
    private static string Hello(params string[] args)
    {
        return "Hello World!";
    }
}
```

### Handling Command Arguments
UnityConsole requires only that arguments passed to a command are whitespace-separated if any. The handling of these arguments is left to you but you should guard against accessing arguments that weren't passed in. For example, the following class defines a static command `connect` that expects a username, password, server and an optional port. Special care is taken so that omitting the username, password or server arguments when invoking the command will gracefully abort its execution.
```csharp
using UnityConsole;

public class HandlingCommandArgumentsExample
{
    [Command("connect", description = "Connect the specified user to the specified server", syntax = "connect username password server [port]")]
    private static string Connect(params string[] args)
    {
        var username = args.Length > 0 ? args[0] : null;
        var password = args.Length > 1 ? args[1] : null;
        var server = args.Length > 2 ? args[2] : null;
        var port = args.Length > 3 ? args[3] : "80"; // default port is 80
        
        if (username == null || password == null || server == null)
        {
            return "Could not connect to server because the authentication information is invalid."; //gracefully abort the command execution
        }
        else
        {
            return MyServerAPI.Connect(username, password, server, port);
        }
    }
}
```

## World space UI
You can use the console in world space too! Simply set your Canvas Render Mode to World Space. You may also need to scale down the Canvas. In the future, I want to implement great VR support. There's an alpha VR demo [available here](http://wenzil.github.io/UnityConsole/).

## Appearance
You can easily change the appearance of the console by changing the images, colors, font styles and state transitions used by the UI elements. It is also possible to anchor the console to any side of the screen.

## API Reference
Quite a bit more information and code samples can be found in the [UnityConsole API Reference](http://wenzil.github.io/UnityConsole/).

## Known Issues
- Before Unity has closed and reopened a new project at least once, the console input is not activated at the start of the game even in the case where it should (i.e. when ConsoleUI is enabled and ConsoleUI.activateInputFieldOnToggle is set to true in the inspector)
- Attempting to navigate the input history when it is empty clears the console input

## Get in touch
[@Syncopath1 on Twitter](https://twitter.com/Syncopath1)

[Syncopath on Reddit](http://www.reddit.com/user/Syncopath)
