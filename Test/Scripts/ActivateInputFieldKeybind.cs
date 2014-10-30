using UnityEngine;

[AddComponentMenu("UnityConsole/Test/Activate Input Field Keybind")]
public class ActivateInputFieldKeybind : MonoBehaviour
{
    public KeyCode activationKey = KeyCode.Tab;
    public ActivateInputFieldAction action;

    void Update()
    {
        if (Input.GetKey(activationKey))
            action.Execute();
    }
}
