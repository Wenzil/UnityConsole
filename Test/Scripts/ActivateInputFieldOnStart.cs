using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ActivateInputFieldOnStart : MonoBehaviour
{
    public InputField inputField;

	void Start() 
    {
        Invoke("ActivateInputField", 0.1f);
	}
	
    private void ActivateInputField()
    {
        inputField.ActivateInputField();
    }
}
