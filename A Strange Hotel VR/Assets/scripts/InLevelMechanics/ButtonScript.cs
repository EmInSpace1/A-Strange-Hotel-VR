using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonScript : MonoBehaviour
{
    [SerializeField] private UnityEvent buttonPressed;
    [SerializeField] private UnityEvent buttonReleased;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("button"))
        {
            buttonPressed.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("button"))
        {
            buttonReleased.Invoke();
        }
    }

    public void TestButton(string output)
    {
        Debug.Log(output);
    }
}
