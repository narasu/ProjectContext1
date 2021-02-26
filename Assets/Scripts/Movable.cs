using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : Interactable
{
    private Rigidbody rb;
    Collider collider;
    bool isGrabbed;


    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }

    //called from InteractingState.Update()
    public override void HandleInteraction()
    {
        base.HandleInteraction();

    }

    // Set position to player's hand, and set all velocities to zero.
    public void Grab()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(Player.Instance.Hand);
        //transform.localPosition = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = false;
        collider.isTrigger = true;
        isGrabbed = true;



        Color c = GetComponent<Renderer>().material.color;
        c.a = 0.5f;
        GetComponent<Renderer>().material.color = c;
    }

    // turn the gravity back on
    public void Drop()
    {
        transform.SetParent(null);
        //rb.useGravity = true;
        //rb.constraints = RigidbodyConstraints.None;
        collider.isTrigger = false;
        isGrabbed = false;
        Color c = GetComponent<Renderer>().material.color;
        c.a = 1.0f;
        GetComponent<Renderer>().material.color = c;
    }
}