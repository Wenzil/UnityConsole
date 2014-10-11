using UnityEngine;
using UnityConsole;

[AddComponentMenu("UnityConsole/Test/Activate Input Field On Start")]
public class ActivateInputFieldOnStart : MonoBehaviour 
{
    public ActivateInputFieldAction action;

	void Start() 
    {
        action.Execute();
	}
}
