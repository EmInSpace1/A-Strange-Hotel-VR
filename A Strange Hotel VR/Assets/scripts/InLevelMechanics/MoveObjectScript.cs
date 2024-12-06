using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObjectScript : MonoBehaviour
{
    [SerializeField] private GameObject wall;
    [SerializeField] private Transform newLocation;
    private Vector3 startPos;

    private bool moveDown;
    private bool moveUp;

    private void Start()
    {
        startPos = wall.transform.position;
    }

    private void FixedUpdate()
    {
        if (moveDown)
            wall.transform.position = Vector3.Lerp(wall.transform.position, newLocation.position, Time.deltaTime);
        if (moveUp)
            wall.transform.position = Vector3.Lerp(wall.transform.position, startPos, Time.deltaTime);
    }
    public void MoveDown()
    {
        moveDown = true;
        moveUp = false;
    }

    public void MoveUp()
    {
        moveUp = true;
        moveDown = false;
    }
}
