using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : Interactable
{
    private Rigidbody rb;
    Collider collider;
    bool isGrabbed;
    Vector3 extraLocalPos;
    Transform playerTransform;
    PlayerTransformInHand transformInHand;
    public bool unchangedRotation = true;

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
        playerTransform = FindObjectOfType<Player>().transform;
        transformInHand = FindObjectOfType<PlayerTransformInHand>();
    }

    //called from InteractingState.Update()
    public override void HandleInteraction()
    {
        base.HandleInteraction();
    }

    public override void HighlightInteraction()
    {
        Debug.Log("highlihgt");
        gameObject.GetComponent<Outline>().enabled = !gameObject.GetComponent<Outline>().enabled;
    }

    // Set position to player's hand, and set all velocities to zero.
    public void Grab()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(Player.Instance.Hand.transform.parent.transform.parent.GetChild(1));
        //transform.localPosition = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = true;
        collider.isTrigger = true;
        isGrabbed = true;


        Color c = GetComponent<Renderer>().material.color;
        c.a = 0.5f;
        GetComponent<Renderer>().material.color = c;
    }

    void Update()
    {
        //Debug.Log(transform.localRotation.eulerAngles);
        if(isGrabbed)
        {
            transform.position = new Vector3(transform.position.x, Player.Instance.Hand.transform.position.y, transform.position.z);
        }
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