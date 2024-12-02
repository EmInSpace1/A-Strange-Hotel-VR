using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VacuumBehaviour : MonoBehaviour
{
    [Header("Pick Up and Let Go actions")]
    [SerializeField] private InputActionProperty pickUpObject;
    [SerializeField] private InputActionProperty shootObject;
    [SerializeField] private float handSpeedMultiplier = 50;
    [SerializeField] private LayerMask grabLayer;

    [Header("Holding Information")]
    [SerializeField] private Transform holdPoint;
    [SerializeField] private float grabDistance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float maxDistance;
    [SerializeField] private float shootSpeed;

    private bool closingHand;
    private bool tryingToShoot;
    private bool holding;
    private bool canPickUp;
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
        tryingToShoot = shootObject.action.ReadValue<float>() == 1;

        Grabbing();

        if (holding && !closingHand)
        {
            //turn on physics on the object
            heldBody.useGravity = true;

            //set the velocity of the object to what is basically the current speed of the hand.
            //the number is set to what feels the best to me.
            Vector3 direction = holdPoint.position - heldObject.transform.position;
            float distance = Vector3.Distance(holdPoint.position, heldObject.transform.position);

            if (distance > 1)
                distance = 1;

            heldBody.velocity = direction.normalized * moveSpeed * distance;

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

        if(holding)
            MoveObject();

        if(holding && tryingToShoot)
        {
            //turn on physics on the object
            heldBody.useGravity = true;

            Vector3 direction = (heldObject.transform.position - transform.position).normalized;
            float distance = Vector3.Distance(transform.position, heldObject.transform.position);

            heldBody.velocity = direction.normalized * shootSpeed;

            //clear all references.
            heldObject = null;
            heldBody = null;
            holding = false;
        }

        if (!closingHand && !canPickUp)
            canPickUp = true;
    }

    private void FixedUpdate()
    {
        //calculate the distance from the last position, used for the object speed when let go.
        if(heldObject != null)
            handSpeed = holdPoint.position - heldObject.transform.position;
        lastPosition = holdPoint.position;
    }

    private void Grabbing()
    {
        RaycastHit hit;

        if (closingHand && !holding && canPickUp && Physics.Raycast(transform.position, transform.forward, out hit, grabDistance, grabLayer))
        {
            ///set all the refences
            heldObject = hit.transform.gameObject;
            holding = true;
            canPickUp = false;

            heldBody = heldObject.GetComponent<Rigidbody>();
            heldBody.useGravity = false;
        }
    }

    private void MoveObject()
    {
        Vector3 direction = holdPoint.position - heldObject.transform.position;
        float distance = Vector3.Distance(holdPoint.position, heldObject.transform.position);

        if (distance > 1)
            distance = 1;

        if (distance >= maxDistance)
        {
            heldBody.velocity = direction.normalized * moveSpeed*distance;
        }
        else heldBody.velocity = Vector3.zero;
    }
}
