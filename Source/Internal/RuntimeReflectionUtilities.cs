using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityConsole.Internal
{
    // Some reflection utilities that can be AOT compiled (and are therefore available at runtime).
    internal class RuntimeReflectionUtilities
    {
        // The assemblies to reflect upon.
        public static readonly ICollection<Assembly> assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };

        // Returns all methods that have the given attribute type in the current assemblies.
        public static IEnumerable<MethodInfo> GetAllMethodsWithAttribute<A>(BindingFlags bindingAttr)
        {
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type.IsClass
                   from method in type.GetMethods(bindingAttr)
                   where Attribute.IsDefined(method, typeof(A))
                   select method;
        }

        // Counts the number of types in the current assemblies.
        public static int CountAllTypes()
        {
            return GetAllTypes().Count();
        }

        // Returns all types in the current assemblies.
        public static IEnumerable<Type> GetAllTypes()
        {
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type.IsClass
                   select type;
        }
    }
}