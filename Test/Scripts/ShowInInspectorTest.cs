using UnityEngine;
using UnityConsole.Internal;

[AddComponentMenu("UnityConsole/Test/Show In Inspector Test")]
public class ShowInInspectorTest : MonoBehaviour
{
    [ShowInInspector(Label = "Property Label Change")]
    public string propertyLabel;

    [ShowInInspector(LabelAtRuntime = "Property Label At Runtime")]
    public string propertyLabelAtDesignTime;

    [ShowInInspector(DisableAtRuntime = true)]
    public string somePropertyToDisableAtRuntime;

    [ShowInInspector(HideAtRuntime = true)]
    public string somePropertyToHideAtRuntime;
}