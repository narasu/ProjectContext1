using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : Interactable
{

    [SerializeField] private Rigidbody rb;
    [SerializeField] private Transform playerHand;
    [SerializeField] private Transform worldType;

    protected override void Awake()
    {
        base.Awake();
    }

    //called from InteractingState.Update()
    public override void HandleInteraction()
    {
        base.HandleInteraction();

    }

    // Set position to player's hand, and set all velocities to zero.
    // layer 9 = in hand. layer 10 = interactable.
    public void Grab()
    {
        gameObject.layer = 9;

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.parent = playerHand;
        transform.localPosition = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
    }

    // Return it to the world layer and turn the gravity back on
    public void Drop()
    {
        transform.parent = worldType;
        rb.useGravity = true;
        rb.constraints = RigidbodyConstraints.None;
        gameObject.layer = 10;
        
    }
}