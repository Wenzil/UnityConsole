using UnityEngine;
using System;
using System.Linq;
using System.Collections.Generic;

namespace Wenzil.Console
{
	/// <summary>
	/// The behavior of the Console.
	/// </summary>
	[DisallowMultipleComponent]
	[RequireComponent(typeof(ConsoleController))]
	public class ConsoleController : MonoBehaviour
	{
		public ConsoleUI ui;
		public KeyCode toggleKey = KeyCode.BackQuote;
		public bool closeOnEscape = false;

        private ConsoleInputHistory inputHistory = new ConsoleInputHistory(5);

		void OnEnable()
		{
			Console.OnConsoleLog += ui.AddNewOutputLine;
			ui.onSubmitCommand += ExecuteCommand;
            ui.onClearConsole += inputHistory.Clear;
		}

		void OnDisable()
		{
			Console.OnConsoleLog -= ui.AddNewOutputLine;
			ui.onSubmitCommand -= ExecuteCommand;
            ui.onClearConsole -= inputHistory.Clear;
		}

		void Update()
		{
            if (Input.GetKeyDown(toggleKey))
                ui.ToggleConsole();
            else if (Input.GetKeyDown(KeyCode.Escape) && closeOnEscape)
                ui.CloseConsole();
            else if (Input.GetKeyDown(KeyCode.UpArrow))
                NavigateInputHistory(true);
            else if (Input.GetKeyDown(KeyCode.DownArrow))
                NavigateInputHistory(false);
		}

        private void NavigateInputHistory(bool up)
        {
            string navigatedToInput = inputHistory.Navigate(up);
            ui.SetInputText(navigatedToInput);
        }

		private void ExecuteCommand(string input)
		{
			string[] parts = input.Split(' ');
			string command = parts[0];
			string[] args = parts.Skip(1).ToArray();
		
			Console.Log("> " + input);
			Console.Log(ConsoleCommandsDatabase.ExecuteCommand(command, args));
            inputHistory.AddNewInputEntry(input);
		}
    }
}