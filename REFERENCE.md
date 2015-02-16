# UnityConsole
```namespace UnityConsole```

This namespace contains the main UnityConsole components.
```csharp
using UnityConsole;
```

## Command
```public struct Command```

Command information and execution callback.

### name
```public string name { get; private set; }```

The command identifier used to access and execute the command.

### callback
```public Command.Callback callback { get; private set; }```

The command execution callback, i.e. the method to call when the command is executed.

### description
```public string description { get; private set; }```

A short description describing what the command does.

### syntax
```public string syntax { get; private set; }```

Syntax information for the command arguments.

### Command()
```public Command(string name, Command.Callback callback, string description, string syntax)```

Constructs a Command with the given name, execution callback, description and syntax information.
```csharp
var command = new Command("join", args => string.Join(", ", args), "Join the string arguments together.", "join args");
```

### ToString()
```public override string ToString()```

Returns a string representation of the command in the format: name - description

## Command.Callback
```public delegate string Command.Callback(params string[] args)```

The command execution callback signature.
#### Parameters
- *args* - The command arguments

#### Return Value
The command response

## CommandAttribute
```public class CommandAttribute : Attribute, _Attribute```

Indicates that a method will act as a console command. The method must be static and compatible with the *Command.Callback* delegate signature. When you apply this attribute to such a method, it will be registered with the console automatically at runtime.
#### See Also
- [Command.Callback](#unityconsole-namespace-commandcallback)

### name
```public string name { get; private set; }```

The label that refers to the command. Can be used from the console input to execute the command. 

### description
```public string description { get; set; }```

A short description describing what the command does.

### syntax
```public string syntax { get; set; }```

Syntax information for the command arguments.

### overrideRegistered
```public bool overrideRegistered { get; set; }```

Whether to override the command that is already registered with the same name (if there is one)

## CommandDatabase
```public static class CommandDatabase```

An utility for registering commands with the console.

### commandNames
```public static IEnumerable<string> commandNames { get; }```

Returns all the command names in alphabetical order.

### RegisterCommand()
```public static void RegisterCommand(string command, Command.Callback callback, string description, string syntax)```

Registers a command with the console. If a command with the same name already exists, it is replaced with the new one.
#### Parameters
- *command* - The name of the command
- *callback* - The command execution callback, i.e the method to call when the command is executed
- *description* - A short description describing what the command does
- *syntax* - Syntax information for the command arguments

### RegisterCommand()
```public static void RegisterCommand(string command, Command.Callback callback, string description, string syntax, bool overrideExisting)```

Registers a command with the console. If a command with the same name already exists, it is replaced with the new one.
#### Parameters
- *command* - The name of the command
- *callback* - The command execution callback, i.e the method to call when the command is executed
- *description* - A short description describing what the command does
- *syntax* - Syntax information for the command arguments
- *overrideExisting* - Whether to override the command that is already registered with the same name (if there is one)

### GetCommand()
```public static Command GetCommand(string command)```

Retrieve the given command by name.

#### Return Value
The retrieved command
#### Exceptions
- *NoSuchCommandException* - Thrown when there is no such command

### HasCommand()
```public static bool HasCommand(string command)```

Returns whether a command with the given name exists.

## Console
```public static class Console```

Static representation of the console. Use Console.Log() anywhere in your code to log messages.

### onLog
```public static event Action<string> onLog```

Occurs whenever a new message is logged.

### onClear
```public static event Action onClear```

Occurs whenever the console is cleared.

### ExecuteCommand()
```public static string ExecuteCommand(string input)```

Parses the given command input and executes it with the parsed arguments.
#### Parameters
- *input* - The raw command input string for the command (may contain arguments to be parsed)

#### Return Value
The command response

### ExecuteCommand()
```public static string ExecuteCommand(string command, params string[] args)```

Executes the given command with the given command arguments.
#### Parameters
- *command* - The name of the command to execute
- *args* - The command arguments

#### Return Value
The command response

### Log()
```public static void Log(string message)```

Logs the given message.

### Clear()
```public static void Clear()```

Clears the console.

## ConsoleController
```public class ConsoleController : MonoBehaviour```

The behaviour of the console.

### toggleKey
```public KeyCode toggleKey```

The keyboard shortcut for opening and closing the console.

### closeOnEscape
```public bool closeOnEscape```

Determines whether or not to close the console when pressing the Escape key on the keyboard.

### inputHistoryCapacity
```public int inputHistoryCapacity```

The maximum capacity for the console input history. Older input entries will be thrown away.

## ConsoleInputHistory
```public class ConsoleInputHistory```

Utility for caching and navigating recently executed console commands.
#### Remarks
When initiating navigation up (after a new input entry was submitted or the console was cleared), we navigate to the last submitted input entry. 
When initiating navigation down (after a new input entry was submitted or the console was cleared), we navigate BELOW the last submitted input entry. 
When navigating up, we navigate ABOVE the last navigated-to input entry. 
When navigating down, we navigate BELOW the last navigated-to input entry.

### ConsoleInputHistory()
```public ConsoleInputHistory(int maxCapacity)```

Constructs the console input history with the given maximum capacity.

### NavigateUp()
```public string NavigateUp()```

Navigate (or initiate navigation) up the input history

#### Return Value
The navigated-to input entry

### NavigateDown()
```public string NavigateDown()```

Navigate (or initiate navigation) down the input history

#### Return Value
The navigated-to input entry

### AddNewInputEntry()
```public void AddNewInputEntry(string input)```

Adds a new input entry to the input history.

### Clear()
```public void Clear()```

Clears the input history and resets its navigation.

## ConsoleUI
```public class ConsoleUI : MonoBehaviour```

The visual component of the console.

### onToggle
```public event Action<bool> onToggle```

Occurs when the console is opened or closed.

### onSubmitInput
```public event Action<string> onSubmitInput```

Occurs when an input entry is submitted by the user.

### isOpen
```public bool isOpen { get; }```

Indicates whether the console is currently open or close.

### activateInputFieldOnOpen
```public bool activateInputFieldOnOpen```

Indicates whether or not to activate the console input when opening the console.

### Toggle()
```public void Toggle()```

Opens or closes the console.

### Open()
```public void Open()```

Opens the console.

### Close()
```public void Close()```

Closes the console.

### OnSubmitInput()
```public void OnSubmitInput(string input)```

Clears/reactivates the console input, scrolls to the bottom of the console output and triggers the onSubmitInput event.

### ActivateInputField()
```public void ActivateInputField()```

Activates the console input, allowing for user submitted input.

### ClearInput()
```public void ClearInput()```

Clears the console input.

### SetInput()
```public void SetInput(string input)```

Writes the given string into the console input, ready to be user submitted.

### HighlightInput()
```public void HighlightInput()```

Selects and highlights the text in the console input

### ClearOutput()
```public void ClearOutput()```

Clears the console output.

### AddNewOutputEntry()
```public void AddNewOutputEntry(string message)```

Displays the given message as a new entry in the console output.

## ExcludeCommandAttribute
```public class ExcludeCommandAttribute : Attribute, _Attribute```

Indicates that a command will not be registered with the console automatically at runtime.

## InvalidCommandSignatureException
```public class InvalidCommandSignatureException : Exception, ISerializable, _Exception```

An exception thrown when a static command has an invalid method signature

### command
```public MethodInfo command { get; private set; }```

The command with the invalid method signature.

## NoSuchCommandException
```public class NoSuchCommandException : Exception, ISerializable, _Exception```

An exception thrown when attempting to retrieve a command that does not exist.

### command
```public string command { get; private set; }```

The command that does not exist.

# UnityConsole.Commands
```namespace UnityConsole.Commands```

This namespace contains the built-in UnityConsole commands.
```csharp
using UnityConsole.Commands;
```

## CommandsCommand
```public static class CommandsCommand```

A definition for the COMMANDS command.

### Commands()
```public static string Commands(params string[] args)```

Displays the list of available commands.

## HelpCommand
```public static class HelpCommand```

A definition for the HELP command.

### Help()
```public static string Help(params string[] args)```

Displays general syntax information or specific command usage.

## QuitCommand
```public static class QuitCommand```

A definition for the QUIT command.

### Quit()
```public static string Quit(params string[] args)```

Quits the application.

# Warnings


CSharpToMarkdown executed in 62 milliseconds
