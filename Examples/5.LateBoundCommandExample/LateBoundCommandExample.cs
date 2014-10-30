using UnityEngine;
using UnityEngine.UI;
using UnityConsole;
using System.Diagnostics;

namespace UnityConsole.Examples
{
    // An example definition (and registration) of a late bound command named TOGGLE_UI
    [AddComponentMenu("UnityConsole/Examples/LateBoundCommandExample")]
    public class LateBoundCommandExample : MonoBehaviour
    {
        [Header("Notice the direct dependency on the UI Canvas in the scene.")]
        public Canvas UI;

        // Manually register our late bound command
        void Start()
        {
            CommandDatabase.RegisterCommand("TOGGLE_UI", ToggleUI, "Toggles the UI visibility", "TOGGLE_UI");
        }

        // Define a command whose job is to toggle the UI visibility. Notice that we don't need to apply the CommandAttribute attribute for late bound commands.
        public string ToggleUI(params string[] args)
        {
            UI.enabled = !UI.enabled;
            return "UI visibility turned " + (UI.enabled ? "on" : "off");
        }
    }
}