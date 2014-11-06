using UnityEngine;

namespace UnityConsole.Commands
{
    /// <summary>
    /// A definition for the QUIT command.
    /// </summary>
    public static class QuitCommand
    {
        /// <summary>
        /// Quits the application.
        /// </summary>
        [Command("QUIT", description = "Quits the application.", syntax = "QUIT", overrideRegistered = false)]
        public static string Quit(params string[] args)
        {
            Application.Quit();
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
            return "Quitting...";
        }
    }
}