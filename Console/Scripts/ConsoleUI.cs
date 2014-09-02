using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public delegate void OnSubmitCommand(string input);
public delegate void OnOpenConsole();
public delegate void OnCloseConsole();
public delegate void OnToggleConsole(bool isConsoleOpen);

/// <summary>
/// The interactive front-end of the Console.
/// </summary>
public class ConsoleUI : MonoBehaviour, IScrollHandler
{
    public OnToggleConsole onToggleConsole;
    public OnSubmitCommand onSubmitCommand;

    public bool isConsoleOpen { get; private set; }

    public Scrollbar scrollbar;
    public Text outputText;
    public InputField inputField;
    public CanvasGroup togglableContent;

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
        Invoke("ActivateInputField", 0.1f); // have to delay, otherwise the enter key writes a newline into the freshly cleared input field
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
        togglableContent.alpha = open ? 1 : 0;

        ClearInput();
        if (open)
            Invoke("ActivateInputField", 0.1f); // have to delay, otherwise the toggle key is written into the input field
        else
            DeactivateInputField();

        if (onToggleConsole != null)
            onToggleConsole(open);
    }

    public void AddNewOutputLine(string line)
    {
        outputText.text += "\n" + line;
    }

    public void ClearOutput()
    {
        outputText.text = "";
    }

    public void ClearInput()
    {
        inputField.value = "";
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
