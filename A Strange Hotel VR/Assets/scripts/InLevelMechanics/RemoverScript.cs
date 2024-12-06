using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RemoverScript : MonoBehaviour
{
    [SerializeField] private UnityEvent onItemRemoved;

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Grabbable"))
        {
            onItemRemoved.Invoke();
            Destroy(other.gameObject);
        }
    }
}
