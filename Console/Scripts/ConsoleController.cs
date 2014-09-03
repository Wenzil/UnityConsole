using UnityEngine;
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
		private List<string> cache = new List<string>();
		private int cachePointer = 0;

		void OnEnable()
		{
			Console.OnConsoleLog += ui.AddNewOutputLine;
			ui.onSubmitCommand += ExecuteCommand;
		}

		void OnDisable()
		{
			Console.OnConsoleLog -= ui.AddNewOutputLine;
			ui.onSubmitCommand -= ExecuteCommand;
		}

		void Start()
		{
			ui.onClearConsole += () => { cache.Clear(); cachePointer = 0; };
		}

		void Update()
		{
			if(Input.GetKeyDown(toggleKey))
				ui.ToggleConsole();
			else if(Input.GetKeyDown(KeyCode.Escape) && closeOnEscape)
				ui.CloseConsole();
			else if(Input.GetKeyDown(KeyCode.UpArrow))
				SetFromCache(true);
			else if(Input.GetKeyDown(KeyCode.DownArrow))
				SetFromCache(false);
		}

		private void SetFromCache(bool upPressed)
		{
			if(cache.Count == 0)
				return;
			ui.SetInput(cache[cachePointer]);
			
            //print(cachePointer);
			if(upPressed)
				cachePointer++;
			else
				cachePointer--;

			cachePointer = Mathf.Clamp(cachePointer, 0, cache.Count - 1);
		}

		private void ExecuteCommand(string input)
		{
			cache.Insert(0, input);
			string[] parts = input.Split(' ');
			string command = parts[0];
			string[] args = parts.Skip(1).ToArray();
		
			Console.Log("> " + input);
			Console.Log(ConsoleCommandsDatabase.ExecuteCommand(command, args));
		}
	}
}