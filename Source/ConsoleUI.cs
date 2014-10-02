using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using System;

namespace UnityConsole
{
	/// <summary>
	/// The visual component of the console.
	/// </summary>
	[DisallowMultipleComponent]
	[RequireComponent(typeof(ConsoleController))]
	public class ConsoleUI : MonoBehaviour
	{
        /// <summary>
        /// Occurs when the console is opened or closed.
        /// </summary>
		public event Action<bool> onToggle;

        /// <summary>
        /// Occurs when an input line is submitted by the user.
        /// </summary>
		public event Action<string> onSubmitInput;

        /// <summary>
        /// The scrollbar used for scrolling the console output.
        /// </summary>
		public Scrollbar scrollbar;

        /// <summary>
        /// The input field used for typing commands.
        /// </summary>
		public InputField consoleInput;

        /// <summary>
        /// The text element displaying the console output.
        /// </summary>
        public Text consoleOutput;

        /// <summary>
        /// The scrollable area for the console output.
        /// </summary>
        public ScrollRect consoleOutputArea;

        /// <summary>
        /// The group (whose visibility can be toggled) aggregating the console UI components.
        /// </summary>
		public CanvasGroup togglableContent;

        /// <summary>
        /// Indicates whether the console is currently open or close.
        /// </summary>
        public bool isConsoleOpen { get; private set; }

		void Awake()
		{
			ToggleConsole(enabled);
		}

		void OnEnable()
		{
			ToggleConsole(true);
		}

		void OnDisable()
		{
			ToggleConsole(false);
		}

        /// <summary>
        /// Opens or closes the console.
        /// </summary>
		public void ToggleConsole()
		{
			ToggleConsole(!isConsoleOpen);
		}

        /// <summary>
        /// Opens or closes the console.
        /// </summary>
        /// <param name="open">Indicates whether to open or close the console</param>
		public void ToggleConsole(bool open)
		{
            bool toggled = isConsoleOpen != open;
			isConsoleOpen = open;
			enabled = open;
			togglableContent.interactable = open;
            togglableContent.blocksRaycasts = open;
			togglableContent.alpha = open ? 1f : 0f;
            consoleInput.gameObject.SetActive(open);
            consoleOutputArea.gameObject.SetActive(open);
            scrollbar.gameObject.SetActive(open);

			ClearInput();
			if(toggled && open)
                this.Invoke(ActivateInput, .1f);

			if(toggled && onToggle != null)
				onToggle(open);
		}

        /// <summary>
        /// Clears/reactivates the console input and scrolls to the bottom of the console output. Also relays the user submitted input to interested listeners (such as ConsoleController).
        /// </summary>
        public void OnSubmitInput(string input)
        {
            if (input.Length > 0)
            {
                if (onSubmitInput != null)
                    onSubmitInput(input);
                ClearInput();
                scrollbar.value = 0;
            }

            // have to delay reactivation because unity's implementation of InputField seems to deactivate it right after OnSubmitInput() returns
            this.Invoke(ActivateInput, .1f);
        }

        public void OnSelectInput()
        {
            Console.Log("Console input selected");
            //ActivateInput();
        }

        public void OnDeselectInput()
        {
            Console.Log("Console input deselected");
        }

        /// <summary>
        /// Activates the console input, allowing for user submitted input.
        /// </summary>
        public void ActivateInput()
        {
            consoleInput.Select();
            consoleInput.ActivateInputField();
            consoleInput.MoveTextEnd(false);
        }

        /// <summary>
        /// Clears the console input.
        /// </summary>
		public void ClearInput()
		{
			SetInputText("");
		}

        /// <summary>
        /// Writes the given string into the console input, ready to be user submitted.
        /// </summary>
		public void SetInputText(string input) 
        {
            consoleInput.MoveTextStart(false);
			consoleInput.value = input;
            consoleInput.MoveTextEnd(false);
		}

        /// <summary>
        /// Selects and highlights the text in the console input
        /// </summary>
        public void HighlightInputText()
        {
            consoleInput.MoveTextStart(false);
            consoleInput.MoveTextEnd(true);
        }

        /// <summary>
        /// Clears the console output.
        /// </summary>
        public void ClearOutput()
        {
            consoleOutput.text = "";
        }

        /// <summary>
        /// Displays the given message as a new entry in the console output.
        /// </summary>
        public void AddNewOutputEntry(string message)
        {
            consoleOutput.text += Environment.NewLine + message;
        }
    }
}