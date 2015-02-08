using UnityEngine;
using CSharpDocumentation;

namespace UnityConsole.Commands
{
    [Summary("A definition for the QUIT command.")]
    public static class QuitCommand
    {
        [Summary("Quits the application.")]
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