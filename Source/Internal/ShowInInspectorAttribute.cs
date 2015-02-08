using UnityEngine;
using CSharpDocumentation;

namespace UnityConsole.Internal
{
    // Customize the property's appearance in the inspector.
    public class ShowInInspectorAttribute : PropertyAttribute
    {
        public string Label { get; set; }
        public string LabelAtRuntime { get; set; }
        public bool DisableAtRuntime { get; set; }
        public bool HideAtRuntime { get; set; }
    }
}