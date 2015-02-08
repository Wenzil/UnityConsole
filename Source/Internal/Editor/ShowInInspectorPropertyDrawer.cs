using UnityEngine;
using UnityEditor;

namespace UnityConsole.Internal
{
    [CustomPropertyDrawer(typeof(ShowInInspectorAttribute))]
    internal class RenameInspectorFieldPropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            ShowInInspectorAttribute showInInspector = attribute as ShowInInspectorAttribute;
            if (Application.isPlaying && showInInspector.HideAtRuntime)
                return 0;
            else
                return base.GetPropertyHeight(property, label);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            ShowInInspectorAttribute showInInspector = attribute as ShowInInspectorAttribute;

            
            string nameAtDesignTime = string.IsNullOrEmpty(showInInspector.Label) ? label.text : showInInspector.Label;
            string nameAtRuntime = string.IsNullOrEmpty(showInInspector.LabelAtRuntime) ? nameAtDesignTime : showInInspector.LabelAtRuntime;
            bool disableAtRuntime = showInInspector.DisableAtRuntime;
            bool hideAtRuntime = showInInspector.HideAtRuntime;
            
            label.text = Application.isPlaying ? nameAtRuntime : nameAtDesignTime;

            EditorGUI.BeginDisabledGroup(Application.isPlaying && disableAtRuntime);
            
            if (!Application.isPlaying || !hideAtRuntime)
                EditorGUI.PropertyField(position, property, label);

            EditorGUI.EndDisabledGroup();
        }
    }
}
