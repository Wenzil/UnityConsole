using UnityEngine;

/// <summary>
/// Centers and hides the mouse cursor. Useful in conjunction with MouseLook and world space UI.
/// </summary>
[AddComponentMenu("UnityConsole/Examples/Lock Default Cursor")]
public class LockDefaultCursor : MonoBehaviour
{
	void Update() 
    {
        // for some reason, setting Screen.lockCursor to true once doesn't center it, but doing it twice does
        Screen.lockCursor = true;
        Screen.lockCursor = false;
        Screen.lockCursor = true;
	}

    void OnDisable()
    {
        Screen.lockCursor = false;
    }
}
