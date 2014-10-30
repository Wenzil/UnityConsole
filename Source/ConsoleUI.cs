using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityConsole.Internal;

namespace UnityConsole
{
    /// <summary>
    /// The visual component of the console.
    /// </summary>
    [DisallowMultipleComponent]
    [AddComponentMenu("UnityConsole/Console UI")]
    public class ConsoleUI : MonoBehaviour
    {
        /// <summary>
        /// Occurs when the console is opened or closed.
        /// </summary>
        public event Action<bool> onToggle;

        /// <summary>
        /// Occurs when an input entry is submitted by the user.
        /// </summary>
        public event Action<string> onSubmitInput;

        /// <summary>
        /// The scrollbar used for scrolling the console output.
        /// </summary>
        public Scrollbar scrollbar;

        /// <summary>
        /// The input field used for typing commands into the console input.
        /// </summary>
        public InputField inputField;

        /// <summary>
        /// The scrollable area for the console output.
        /// </summary>
        public ScrollRect outputArea;

        /// <summary>
        /// The text element displaying the console output.
        /// </summary>
        public Text outputText;

        /// <summary>
        /// Whether or not to activate the console input when opening the console.
        /// </summary>
        public bool activateInputFieldOnToggle = true;

        /// <summary>
        /// Indicates whether the console is currently open or close.
        /// </summary>
        public bool isOpen { get; private set; }

        void Awake()
        {
            Show(false);
        }

        void OnEnable()
        {
            Toggle(true);
        }

        void OnDisable()
        {
            Toggle(false); 
        }

        /// <summary>
        /// Opens or closes the console.
        /// </summary>
        public void Toggle()
        {
            Toggle(!isOpen);
        }

        /// <summary>
        /// Opens or closes the console.
        /// </summary>
        /// <param name="open">Indicates whether to open or close the console</param>
        public void Toggle(bool open)
        {
            bool toggled = isOpen != open;
            isOpen = open;

            Show(open);

            if (toggled && !open)
                ClearInput();

            if(toggled && open && activateInputFieldOnToggle)
                ActivateInputField();

            if(toggled && onToggle != null)
                onToggle(open);
        }

        private void Show(bool show)
        {
            inputField.gameObject.SetActive(show);
            outputArea.gameObject.SetActive(show);
            scrollbar.gameObject.SetActive(show);
        }

        /// <summary>
        /// Clears/reactivates the console input, scrolls to the bottom of the console output and triggers the OnSubmitInput event.
        /// </summary>
        public void OnSubmitInput(string input)
        {
            if (string.IsNullOrEmpty(input))
                return;

            ClearInput();
            ActivateInputField();
            scrollbar.value = 0;
            if (onSubmitInput != null)
                onSubmitInput(input);
        }

        /// <summary>
        /// Activates the console input, allowing for user submitted input.
        /// </summary>
        public void ActivateInputField()
        {
            inputField.Select();
            inputField.ActivateInputField();
            inputField.MoveTextEnd(false);
        }

        /// <summary>
        /// Clears the console input.
        /// </summary>
        public void ClearInput()
        {
            SetInput("");
        }

        /// <summary>
        /// Writes the given string into the console input, ready to be user submitted.
        /// </summary>
        public void SetInput(string input) 
        {
            inputField.MoveTextStart(false);
            inputField.value = input;
            inputField.MoveTextEnd(false);
        }

        /// <summary>
        /// Selects and highlights the text in the console input
        /// </summary>
        public void HighlightInput()
        {
            // untested
            inputField.MoveTextStart(false);
            inputField.MoveTextEnd(true);
        }

        /// <summary>
        /// Clears the console output.
        /// </summary>
        public void ClearOutput()
        {
            outputText.text = "";
            outputText.SetLayoutDirty();
        }

        /// <summary>
        /// Displays the given message as a new entry in the console output.
        /// </summary>
        public void AddNewOutputEntry(string message)
        {
            outputText.text += Environment.NewLine + message;
        }
    }
}