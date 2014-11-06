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
3. Run your scene and press TAB to toggle the Console

## Logging
Anywhere in your code, simply use ```Console.Log()``` to output to the Console.

## Built-in Commands
UnityConsole comes with 3 default commands.
- COMMANDS - displays a list of all available commands
- HELP - displays general help information, or details about the given command
- QUIT - quits the application

To execute a command, simply type into the console input. To know more about some command, type the HELP command followed by the command name you are interested in.

```
HELP quit
```

The console input ignores case and expects arguments to be separated by whitespace.

## Registering Static Commands
UnityConsole allows you to define your own commands. Commands defined in static methods can be elegantly registered with the console:

1. Create a static method compatible with the ```string Command.Callback(string[] args)``` delegate signature
2. Apply the [Command] attribute to the method, specifying the command name, description and syntax

```csharp
using UnityConsole;

public class StaticCommandExample
{
    // Define a static command whose job is to output HELLO WORLD
    [Command("HELLO", description = "Outputs \"HELLO WORLD\".", syntax = "HELLO")]
    private static string Hello(params string[] args)
    {
        // the return value is the command output
        return "HELLO WORLD";
    }
}
```

Since the command is static and is decorated with the [Command] attribute, it will be registered with the console automatically at runtime.

## Registering Late Bound Commands
Non-static commands need to be registered manually at runtime. It can be done at any point, hence the term "late bound". A good place to do it is the Start() method of a script.

```csharp
using UnityConsole;

public class LateBoundCommandExample : MonoBehaviour
{
    // Notice the direct dependency on the UI Canvas in the scene.
    public Canvas UI;

    // Manually register our late bound command at the start of the game or whenever this object is initialized
    void Start()
    {
        CommandDatabase.RegisterCommand("TOGGLE_UI", ToggleUI, "Toggles the UI visibility", "TOGGLE_UI");
    }

    // Define a command whose job is to toggle the UI visibility.
    public string ToggleUI(params string[] args)
    {
        UI.enabled = !UI.enabled;
        return "UI visibility turned " + (UI.enabled ? "on" : "off");
    }
}
```

Unlike static commands, late bound commands allow for direct dependencies on gameobjects in the scene. 

## World space UI
You can use the console in world space too! Simply set your Canvas Render Mode to World Space and you're good to go. You may need to scale down the Canvas. In the future, I want to implement great VR support. There's an alpha VR demo [available here](http://wenzil.github.io/UnityConsole/).

## Appearance
You can easily change the appearance of the console by changing the images, colors, font styles and state transitions used by the UI elements. It is also possible to anchor the Console to any side of the screen.

## API Reference
Quite a bit more information and code samples can be found in the [UnityConsole API Reference](http://wenzil.github.io/UnityConsole/).

## Known Issues
- Console input gets deactivated and submitted when, after the console is opened, the mouse cursor moves for the first time. See [this bug submission](http://issuetracker.unity3d.com/issues/input-field-selection-is-immediately-deactivated-after-moving-mouse)
- Console input gets deactivated and submitted when it has just been activated by navigation keys and the mouse cursor moves. See [this bug submission](http://issuetracker.unity3d.com/issues/moving-cursor-unselects-whatever-was-selected-with-the-ui-navigation-keys)
- Before Unity has closed and reopened a new project at least once, the console input is not activated at the start of the game even in the case where it should (i.e. when ConsoleUI is enabled and ConsoleUI.activateInputFieldOnToggle is set to true in the inspector)
- Attempting to navigate the input history when it is empty clears the console input

## Get in touch
[@Syncopath1 on Twitter](https://twitter.com/Syncopath1)

[Syncopath on Reddit](http://www.reddit.com/user/Syncopath)