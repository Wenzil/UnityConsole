using UnityEngine;
using CSharpDocumentation;

namespace UnityConsole
{
    [Summary("The behaviour of the console.")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ConsoleUI))]
    [AddComponentMenu("UnityConsole/Console Controller")]
    public class ConsoleController : MonoBehaviour
    {
        // The visual component of the console.
        [SerializeField]
        private ConsoleUI ui;

        [Summary("The keyboard shortcut for opening and closing the console.")]
        [SerializeField]
        public KeyCode toggleKey = KeyCode.Tab;

        [Summary("Determines whether or not to close the console when pressing the Escape key on the keyboard.")]
        [SerializeField]
        public bool closeOnEscape = true;

        [Summary("The maximum capacity for the console input history. Older input entries will be thrown away.")]
        [SerializeField]
        public int inputHistoryCapacity = 100;

        private ConsoleInputHistory inputHistory;

        void Awake()
        {
            inputHistory = new ConsoleInputHistory(inputHistoryCapacity);
        }

        private void OnEnable()
        {
            Console.onLog += OnLog;
            Console.onClear += OnClear;
            ui.onSubmitInput += OnSubmitInput;
        }

        private void OnDisable()
        {
            Console.onLog -= OnLog;
            Console.onClear -= OnClear;
            ui.onSubmitInput -= OnSubmitInput;
        }

        private void OnLog(string message)
        {
            ui.AddNewOutputEntry(message);
        }

        [ContextMenu("Clear Console")]
        private void OnClear()
        {
            ui.ClearOutput();
            inputHistory.Clear();
        }

        private void OnSubmitInput(string input)
        {
            Console.ExecuteCommand(input);
            inputHistory.AddNewInputEntry(input);
        }

        void Update()
        {
            if (Input.GetKeyDown(toggleKey))
                ui.Toggle();
            else if (Input.GetKeyDown(KeyCode.Escape) && closeOnEscape)
                ui.Close();
            else if (Input.GetKeyDown(KeyCode.UpArrow) && ui.isOpen)
                NavigateInputHistoryUp();
            else if (Input.GetKeyDown(KeyCode.DownArrow) && ui.isOpen)
                NavigateInputHistoryDown();
        }

        private void NavigateInputHistoryUp()
        {
            string navigatedToInput = inputHistory.NavigateUp();
            ui.SetInput(navigatedToInput);
        }

        private void NavigateInputHistoryDown()
        {
            string navigatedToInput = inputHistory.NavigateDown();
            ui.SetInput(navigatedToInput);
        }
    }
}