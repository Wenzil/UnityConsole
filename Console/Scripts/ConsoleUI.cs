using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

namespace Wenzil.Console
{

	/// <summary>
	/// The interactive front-end of the Console.
	/// </summary>
	[DisallowMultipleComponent]
	[RequireComponent(typeof(ConsoleController))]
	public class ConsoleUI : MonoBehaviour, IScrollHandler
	{
		public event Action<bool> onToggleConsole;
		public event Action<string> onSubmitCommand;
		public event Action onClearConsole;

		public bool isConsoleOpen { get; private set; }

		public Scrollbar scrollbar;
		public Text outputText;
		public InputField inputField;
		public CanvasGroup togglableContent;

		[SerializeField, Range(0f, 1f)]
		private float onAlpha = 0.75f;

		void Awake()
		{
			if (!enabled)
				CloseConsole();
		}

		void OnEnable()
		{
			inputField.onSubmit.AddListener(OnSubmit);
			OpenConsole();
		}

		void OnDisable()
		{
			inputField.onSubmit.RemoveListener(OnSubmit);
			CloseConsole();
		}

		public void OnSubmit(string input)
		{
			if (input.Length > 0)
			{
				if (onSubmitCommand != null)
					onSubmitCommand(input);
				scrollbar.value = 0;
				ClearInput();
			}
		
			// have to delay, otherwise the enter key writes a newline into the freshly cleared input field
			this.Invoke(ActivateInputField, 0.1f);
		}
	
		public void OnScroll(PointerEventData eventData)
		{
			scrollbar.value += eventData.scrollDelta.y;
		}

		public void OpenConsole()
		{
			ToggleConsole(true);
		}

		public void CloseConsole()
		{
			ToggleConsole(false);
		}

		public void ToggleConsole()
		{
			ToggleConsole(!isConsoleOpen);
		}

		public void ToggleConsole(bool open)
		{
			isConsoleOpen = open;
			enabled = open;
			togglableContent.interactable = open;
			togglableContent.alpha = open ? onAlpha : 0f;

			ClearInput();
			if(open)
				this.Invoke(ActivateInputField, 0.1f); // have to delay, otherwise the toggle key is written into the input field
			else
				DeactivateInputField();

			if(onToggleConsole != null)
				onToggleConsole(open);
		}

		public void AddNewOutputLine(string line)
		{
			outputText.text += Environment.NewLine + line;
		}

		public void ClearOutput()
		{
			outputText.text = "";
			if(onClearConsole != null)
				onClearConsole();
		}

		public void ClearInput()
		{
			SetInput("");
		}

		public void SetInput(string input) 
        {
			inputField.value = input;
		}

		public void ActivateInputField()
		{
			EventSystemManager.currentSystem.SetSelectedGameObject(scrollbar.gameObject, null);
			EventSystemManager.currentSystem.SetSelectedGameObject(inputField.gameObject, null);
			inputField.OnPointerClick(null);
		}

		public void DeactivateInputField()
		{
			if (EventSystemManager.currentSystem != null) // necessary when console is being destroyed as a result of app shutdown
				EventSystemManager.currentSystem.SetSelectedGameObject(null, null);
		}
	}
}