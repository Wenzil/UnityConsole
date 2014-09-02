# UnityConsole
Welcome to UnityConsole, an easy-to-use developer console for Unity 4.6!

![Screenshot](https://dl.dropboxusercontent.com/u/106740647/UnityConsole/Screenshot.jpg)

## Getting Started
1. Import the UnityConsole package into your project (or copy the UnityConsole directory into your Assets folder)
2. Add a UI canvas to your scene if you don't already have one (GameObject > UI > Canvas)
3. Drag-and-Drop the Console prefab onto the Canvas in the Hierarchy
4. Run your scene and press TAB to toggle the Console

## Logging
Anywhere in your code, simply use ```Console.Log()``` to output to the Console

## Registering Commands
Use the ```ConsoleCommandsDatabase.RegisterCommand()``` method to register your own commands. Here's an example.

```csharp
public class CustomCommands : MonoBehaviour
{
    void Start()
    {
        ConsoleCommandsDatabase.RegisterCommand("HELP", CustomCommands.Help);
        ConsoleCommandsDatabase.RegisterCommand("QUIT", CustomCommands.Quit);
    }

    private static string Help(params string[] args)
    {
        return "It is dangerous to go alone! Take this."
    }

    private static string Quit(params string[] args)
    {
        Application.Quit();
        
#if UNITY_EDITOR
        if (Application.isEditor)
            UnityEditor.EditorApplication.isPlaying = false;
#endif

        return "Quitting...";
    }
```

## World space UI
You can use UnityConsole in world space too! Simply set your Canvas Render Mode to World Space and you're good to go. You may need to scale down the Canvas.

## Skinning
You can easily change the appearance of the Console by changing the image sources, font styles and state transitions of the various UI components. It is also possible to anchor the Console to any side of the screen.

## Known Issues
- The input field sometimes randomly loses focus
- The console output text overflows its rectangle when viewed from behind in World Space render mode

## Get in touch

Feel free to report any issues you may find right here on Github!

@Syncopath1 on Twitter
