using UnityEngine;

namespace UnityConsole
{
    /// <summary>
    /// The behaviour of the console.
    /// </summary>
    [DisallowMultipleComponent]
    [RequireComponent(typeof(ConsoleUI))]
    [AddComponentMenu("UnityConsole/Console Controller")]
    public class ConsoleController : MonoBehaviour
    {
        /// <summary>
        /// The visual component of the console.
        /// </summary>
        public ConsoleUI ui;

        /// <summary>
        /// The keyboard shortcut for opening and closing the console.
        /// </summary>
        public KeyCode toggleKey = KeyCode.Tab;

        /// <summary>
        /// Determines whether or not to close the console when pressing the Escape key on the keyboard.
        /// </summary>
        public bool closeOnEscape = true;

        /// <summary>
        /// The maximum capacity for the console input history. Older input entries will be thrown away.
        /// </summary>
        public int inputHistoryCapacity = 100;

        private ConsoleInputHistory inputHistory;

        void Awake()
        {
            inputHistory = new ConsoleInputHistory(inputHistoryCapacity);
        }

        void OnEnable()
        {
            Console.onLog += OnLog;
            Console.onClear += OnClear;
            ui.onSubmitInput += OnSubmitInput;
        }

        void OnDisable()
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
                ui.Toggle(false);
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