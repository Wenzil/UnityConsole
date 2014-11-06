using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace UnityConsole.Internal
{
    /// <summary>
    /// Some reflection utilities that can be AOT compiled (and are therefore available at runtime).
    /// </summary>
    internal class RuntimeReflectionUtilities
    {
        /// <summary>
        /// The assemblies to reflect upon.
        /// </summary>
        public static readonly ICollection<Assembly> assemblies = new List<Assembly> { Assembly.GetExecutingAssembly() };

        /// <summary>
        /// Returns all methods that have the given attribute type in the current assemblies.
        /// </summary>
        public static IEnumerable<MethodInfo> GetAllMethodsWithAttribute<A>(BindingFlags bindingAttr)
        {
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type.IsClass
                   from method in type.GetMethods(bindingAttr)
                   where Attribute.IsDefined(method, typeof(A))
                   select method;
        }

        /// <summary>
        /// Counts the number of types in the current assemblies.
        /// </summary>
        public static int CountAllTypes()
        {
            return GetAllTypes().Count();
        }

        /// <summary>
        /// Returns all types in the current assemblies.
        /// </summary>
        public static IEnumerable<Type> GetAllTypes()
        {
            return from assembly in assemblies
                   from type in assembly.GetTypes()
                   where type.IsClass
                   select type;
        }
    }
}