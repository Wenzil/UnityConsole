using System.Collections;
using UnityEngine;
using UnityEngine.UI;


[AddComponentMenu("UnityConsole/Test/Activate Input Field Timer")]
public class ActivateInputFieldTimer : MonoBehaviour 
{
    public float time = 1;
    public ActivateInputFieldAction action;

    void OnEnable()
    {
        StartCoroutine("Timer");
    }

    void OnDisable()
    {
        StopCoroutine("Timer");
    }

    private IEnumerator Timer()
    {
        for (float timeLeft = time; timeLeft > 0; timeLeft -= Time.deltaTime)
            yield return null;
        action.Execute();
        gameObject.SetActive(false);
    }
}
