using UnityEngine;
using UnityConsole;

namespace UnityConsole.Examples
{
    // A special utility class that revokes user controls whenever the console is open. Very game-specific.
    [AddComponentMenu("UnityConsole/Examples/Toggle Game Controls On Toggle Console")]
    public class ToggleGameControlsOnToggleConsole : MonoBehaviour
    {
        public ConsoleUI console;
        public MouseLook mouseLook;
        public KeyboardController keyboardController;

        void OnEnable()
        {
            console.onToggle += ToggleMouseLook;
            ToggleMouseLook(console.isOpen);
        }

        void OnDisable()
        {
            console.onToggle -= ToggleMouseLook;
            ToggleMouseLook(false);
        }

        private void ToggleMouseLook(bool isConsoleOpen)
        {
            if (mouseLook != null)
                mouseLook.enabled = !isConsoleOpen;

            if (keyboardController != null)
                keyboardController.enabled = !isConsoleOpen;
        }
    }
}