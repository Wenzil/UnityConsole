using UnityEngine;
using UnityEngine.UI;

[AddComponentMenu("UnityConsole/Test/Activate Input Field Action")]
public class ActivateInputFieldAction : MonoBehaviour
{
    public InputField inputField;

    public void Execute()
    {
        inputField.Select();
        inputField.ActivateInputField();
        inputField.MoveTextEnd(false);
    }
}