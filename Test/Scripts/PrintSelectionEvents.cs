using UnityEngine;
using UnityEngine.EventSystems;

[AddComponentMenu("UnityConsole/Test/Print Selection Events")]
public class PrintSelectionEvents : MonoBehaviour, ISelectHandler, IDeselectHandler
{
    public void OnSelect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name + " selected");
    }

    public void OnDeselect(BaseEventData eventData)
    {
        Debug.Log(gameObject.name + " deselected");
    }
}