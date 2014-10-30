using UnityEngine;
using System;
using UnityConsole;
using UnityConsole.Commands;

namespace UnityConsole.Examples
{
    // Three static command definition examples.
    public class StaticCommandsExample
    {
        // Define a command named HELLO whose job is to output "HELLO WORLD". Comment out the [ExcludeCommand] attribute to actually include this command in your build.
        [ExcludeCommand]
        [Command("HELLO", Description = "Outputs \"HELLO WORLD\".", Syntax = "HELLO")]
        private static string Hello(params string[] args)
        {
            // the return value is the command output
            return "HELLO WORLD";
        }

        // Define a command named SPAWN whose job is to spawn primitive game objects. Comment out the [ExcludeCommand] attribute to actually include this command in your build.
        [ExcludeCommand]
        [Command("SPAWN", Description = "Spawns a new primitive game object with the given name and primitve type.", Syntax = "SPAWN name primitiveType")]
        private static string Spawn(params string[] args)
        {
            // If we got the syntax wrong, simply display help information about SPAWN
            if (args.Length < 2)
                return HelpCommand.Help("SPAWN");

            // Fetch the command arguments
            string name = args[0];
            string primitiveType = args[1];

            PrimitiveType primitiveTypeEnum;
            GameObject gameObject;

            // Parse the given primitive type string into a UnityEngine.PrimitivteType enum
            try
            {
                primitiveTypeEnum = (PrimitiveType)Enum.Parse(typeof(PrimitiveType), primitiveType, true);
            }
            catch
            {
                return "Invalid primitive type: " + primitiveType + ". See UnityEngine.PrimitiveType to learn more.";
            }

            // Spawn the primitive in front of the camera 
            gameObject = GameObject.CreatePrimitive(primitiveTypeEnum);
            gameObject.name = name;
            gameObject.transform.position = Camera.main.transform.position + Camera.main.transform.forward * 5;
            return "Spawned a new " + primitiveTypeEnum + " named \"" + name + "\"";
        }

        // Define a command named KILL whose job is to destroy the specified game objects. Comment out the [ExcludeCommand] attribute to actually include this command in your build.
        [ExcludeCommand]
        [Command("KILL", Description = "Kills the game object with the given name (case sensitive).", Syntax = "KILL name")]
        private static string Kill(params string[] args)
        {
            // If we got the syntax wrong, symply display help information about KILL
            if (args.Length < 1)
                return HelpCommand.Help("KILL");

            // Fetch the single command argument
            string name = args[0];

            // Find and destroy the game object with the specified name if it exists
            GameObject gameObject = GameObject.Find(name);

            if (gameObject == null)
                return "No game object named \"" + name + "\" to kill";

            GameObject.Destroy(gameObject);
            return "Killed game object named \"" + name + "\"";
        }
    }
}