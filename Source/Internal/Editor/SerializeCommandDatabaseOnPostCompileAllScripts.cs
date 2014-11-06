using UnityEngine;
using UnityEditor.Callbacks;

internal static class SerializeCommandDatabaseOnPostCompileAllScripts
{
    [DidReloadScripts]
    private static void OnPostCompileAllScripts()
    {
        // This is an experimental idea I want to investigate:
        // At post compile time, find all static commands in the compiled assemblies through reflection and serialize them. Then at runtime, have the command database deserialize and register them. 
        // I will need to compare runtime performance between the current approach (reflection) vs this approach (deserialization)
    }
}