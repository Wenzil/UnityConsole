using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityConsole;

public class MyUIComponent : Selectable 
{
    public override void OnSelect(BaseEventData eventData)
    {
        base.OnSelect(eventData);
        Console.Log(gameObject.name + " selected");
    }

    public override void OnDeselect(BaseEventData eventData)
    {
        base.OnDeselect(eventData);
        Console.Log(gameObject.name + " deselected");
    }
}
