using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityConsole.Internal;
using CSharpDocumentation;

namespace UnityConsole
{
    [Summary("The visual component of the console.")]
    [DisallowMultipleComponent]
    [AddComponentMenu("UnityConsole/Console UI")]
    public class ConsoleUI : MonoBehaviour
    {
        [Summary("Occurs when the console is opened or closed.")]
        public event Action<bool> onToggle;

        [Summary("Occurs when an input entry is submitted by the user.")]
        public event Action<string> onSubmitInput;

        // The scrollbar used for scrolling the console output.
        [SerializeField]
        private Scrollbar scrollbar;

        // The input field used for typing commands into the console input.
        [SerializeField]
        private InputField inputField;

        // The scrollable area for the console output.
        [SerializeField]
        private ScrollRect outputArea;

        // The text element displaying the console output.
        [SerializeField]
        private Text outputText;

        [Summary("Indicates whether or not to activate the console input when opening the console.")]
        [SerializeField]
        public bool activateInputFieldOnOpen = true;
        
        [Summary("Indicates whether the console is currently open or close.")]
        public bool isOpen { get { return enabled; } }

        private void Awake()
        {
            Show(false);
        }

        private void OnEnable()
        {
            OnToggle(true);
        }

        private void OnDisable()
        {
            OnToggle(false);
        }

        private void OnToggle(bool open)
        {
            Show(open);

            if (!open)
                ClearInput();

            if (open && activateInputFieldOnOpen)
                ActivateInputField();

            if (onToggle != null)
                onToggle(open);
        }

        private void Show(bool show)
        {
            inputField.gameObject.SetActive(show);
            outputArea.gameObject.SetActive(show);
            scrollbar.gameObject.SetActive(show);
        }

        [Summary("Opens or closes the console.")]
        public void Toggle()
        {
            enabled = !enabled;
        }

        [Summary("Opens the console.")]
        public void Open()
        {
            enabled = true;
        }

        [Summary("Closes the console.")]
        public void Close()
        {
            enabled = false;
        }

        [Summary("Clears/reactivates the console input, scrolls to the bottom of the console output and triggers the onSubmitInput event.")]
        public void OnSubmitInput(string input)
        {
            Debug.Log("OnSubmitInput()");
            if (string.IsNullOrEmpty(input))
                return;

            ClearInput();
            ActivateInputField();
            scrollbar.value = 0;
            if (onSubmitInput != null)
                onSubmitInput(input);
        }

        public void OnValidateInput(string input)
        {
            inputField.text = input.TrimEnd(Environment.NewLine.ToCharArray());
        }

        [Summary("Activates the console input, allowing for user submitted input.")]
        public void ActivateInputField()
        {
            inputField.Select();
            inputField.ActivateInputField();
            inputField.MoveTextEnd(false);
        }

        [Summary("Clears the console input.")]
        public void ClearInput()
        {
            SetInput("");
        }

        [Summary("Writes the given string into the console input, ready to be user submitted.")]
        public void SetInput(string input) 
        {
            inputField.MoveTextStart(false);
            inputField.text = input;
            inputField.MoveTextEnd(false);
        }

        [Summary("Selects and highlights the text in the console input")]
        public void HighlightInput()
        {
            // untested
            inputField.MoveTextStart(false);
            inputField.MoveTextEnd(true);
        }

        [Summary("Clears the console output.")]
        public void ClearOutput()
        {
            outputText.text = "";
            outputText.SetLayoutDirty();
        }

        [Summary("Displays the given message as a new entry in the console output.")]
        public void AddNewOutputEntry(string message)
        {
            outputText.text += Environment.NewLine + message;
        }
    }
}