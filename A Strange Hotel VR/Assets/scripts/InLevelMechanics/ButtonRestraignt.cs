using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonRestraignt : MonoBehaviour
{
    private Vector3 startPosition;
    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (transform.position.y > startPosition.y)
            transform.position = startPosition;
    }
}
