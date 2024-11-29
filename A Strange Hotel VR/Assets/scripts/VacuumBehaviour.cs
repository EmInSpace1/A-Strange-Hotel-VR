using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VacuumBehaviour : MonoBehaviour
{
    [Header("Pick Up and Let Go actions")]
    [SerializeField] private InputActionProperty pickUpObject;
    [SerializeField] private float handSpeedMultiplier = 50;
    [SerializeField] private LayerMask grabLayer;

    [Header("Holding Information")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float grabDistance;

    private bool closingHand;
    private bool holding;
    private GameObject heldObject;
    private Rigidbody heldBody;

    private Vector3 handSpeed;
    private Vector3 lastPosition;

    private void Start()
    {
        //this is needed for calculating velocity when letting go of an object.
        lastPosition = transform.position;
    }

    private void Update()
    {
        closingHand = pickUpObject.action.ReadValue<float>() == 1;

        Grabbing();

        if (holding && !closingHand)
        {
            //turn on physics on the object
            heldBody.isKinematic = false;
            heldObject.transform.SetParent(null);

            //set the velocity of the object to what is basically the current speed of the hand.
            //the number is set to what feels the best to me.
            heldBody.velocity = handSpeed * handSpeedMultiplier;

            //clear all references.
            heldObject = null;
            heldBody = null;
            holding = false;
        }

        //clear all refences if the object in one of the hands got destroyed while holding it.
        if (holding && !heldBody)
        {
            heldObject = null;
            heldBody = null;
            holding = false;
        }

    }

    private void Grabbing()
    {
        RaycastHit hit;

        if(closingHand && !holding && Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, grabLayer))
        {
            ///set all the refences
            heldObject = hit.transform.gameObject;
            holding = true;

            heldObject.transform.SetParent(holdPoint);
            heldBody = heldObject.GetComponent<Rigidbody>();

            //turning off physics
            heldBody.isKinematic = true;
        }
    }
}
