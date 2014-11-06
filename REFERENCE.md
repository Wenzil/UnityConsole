UnityConsole API Reference
===============
Below you will find the reference documentation for UnityConsole complete with code samples.

# Installation
Clone the [UnityConsole repository](https://github.com/Wenzil/UnityConsole) and put the root directory into your Assets folder.

Alternatively, download and import one of the following unity packages:

- [UnityConsoleWithExampleScenes](http://wenzil.github.io/UnityConsole/) which includes useful step-by-step learning examples

- [UnityConsoleVRDemoAlpha](http://wenzil.github.io/UnityConsole/)  which includes a basic demo for early stage VR support

- [UnityConsoleMinimal](http://wenzil.github.io/UnityConsole/) which includes nothing but the necessary UnityConsole components

# Getting Started
1. Add a UI canvas to your scene if you don't already have one (GameObject > UI > Canvas)
2. Drag-and-Drop the UnityConsole prefab onto the Canvas in the Hierarchy
3. Run your scene and press TAB to toggle the Console

# API Reference
## UnityConsole namespace
The UnityConsole API is all part of the UnityConsole namespace. To avoid having to specify fully qualified names every time a method contained within is used, simply employ the **using** directive.
```csharp
using UnityConsole;
```

## Console
Static representation of the console.

### Console.onLog

```static event Action<string> onLog```

Occurs whenever a new message is logged.
```csharp
// Invoke SomeMethod() whenever a new message is logged
Console.onLog += () => SomeMethod();
```

### Console.onClear

```static event Action<string> onClear```

Occurs whenever the console is cleared.
```csharp
// Invoke SomeMethod() whenever the console is cleared
Console.onLog += () => SomeMethod();
```

### Console.Log

```static void Log(string message)```

Logs the given message.
```csharp
Console.Log("This message will show up in the console output.");
```

### Console.Clear

```static void Clear()```

Clears the console.
```csharp
Console.Clear();
```

### Console.ExecuteCommand

```static string ExecuteCommand(string input)```

Parses the given command input and executes it with the parsed arguments.

**input** The raw command input string for the command (may contain arguments to be parsed)

**Returns** The command response

```static string ExecuteCommand(string command, params string[] args)```

Executes the given command with the given command arguments.

**command** The name of the command to execute

**args** The command arguments

**Returns** The command response
