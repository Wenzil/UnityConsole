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
To execute a command, simply type it into the console input, followed by its command arguments if any. 

UnityConsole comes with 3 built-in commands.
- *commands* - Displays the list of all available commands
- *help* - Displays general help information, or details about the given command
- *quit* - Quits the application

For example, typing ```help quit``` will execute the *help* command which will display details about the *quit* command

## Registering Static Commands
UnityConsole allows you to define your own commands. Commands defined in static methods can be registered with the console by applying the [CommandAttribute](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-commandattribute) attribute:

1. Define a static method compatible with the predefined [Command.Callback](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-command-callback) delegate signature
2. Apply the [CommandAttribute](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-commandattribute) attribute to the method, specifying the command name, description and syntax

For example, the following method defines a static command whose job is to output "HELLO WORLD".
```csharp
using UnityConsole;

public class StaticCommandExample
{
    [Command("hello", description = "Outputs \"HELLO WORLD\"", syntax = "hello")]
    private static string Hello(params string[] args)
    {
        return "HELLO WORLD";
    }
}
```

The string array parameter contains any command-line arguments passed in. The return value is the command output. Since the command is static and is decorated with the [CommandAttribute](http://wenzil.github.io/UnityConsole/index.html#unityconsole-namespace-commandattribute) attribute, it will be registered with the console automatically at runtime. It can be invoked by typing ```hello``` into the console input. 

## Registering Non-Static Commands
Non-static commands need to be registered manually at runtime. It can be done at any point, but a good place to do it is within the Start() method of a script.

For example, the following script defines a non-static command whose job is to toggle the UI visibility of the game.
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

Unlike static commands, non-static commands require manual registration at runtime but make it easier to reference game objects in the scene. After the command has been registered, it can be invoked by typing ```toggle_ui``` into the console input.

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
