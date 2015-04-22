# UnityConsole
Welcome to UnityConsole, an easy-to-use developer console for Unity 5!

![Screenshot](https://dl.dropboxusercontent.com/u/106740647/UnityConsole/Screenshot.jpg)

## Getting Started
1. Import the UnityConsole package into your project (or clone the UnityConsole repository into your Assets folder)
2. Add a UI canvas to your scene if you don't already have one (GameObject > UI > Canvas)
3. Drag-and-Drop the Console prefab onto the canvas in the Hierarchy
4. Run your scene and press TAB to toggle the console

## Logging
Anywhere in your code, simply use ``Console.Log()`` to output to the console

## Registering Commands
Use the ``ConsoleCommandsDatabase.RegisterCommand()`` method to register your own commands. Here's an example.

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
You can use UnityConsole in world space too! Simply set your canvas Render Mode to World Space and you're good to go. You may need to scale down the canvas.

## Skinning
You can easily change the appearance of the console by changing the image sources, font styles and state transitions of the various UI components. It is also possible to anchor the console to any side of the screen.

## Contributing

Feel free to create pull requests or report any issues you may find. I'll be taking your feedback!

## Contact me

@Syncopath1 on Twitter
