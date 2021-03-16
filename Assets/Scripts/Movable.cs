using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Movable : Interactable
{
    public bool unchangedRotation = true;
    bool isGrabbed;
    Rigidbody rb;
    Collider collider;
    Vector3 extraLocalPos;
    Transform playerTransform;
    Quaternion startRotation;
    PlayerTransformInHand transformInHand;

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
        //Player.Instance.Hand.transform.position = transform.position;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        transform.SetParent(playerTransform.GetChild(1).transform);
        //transform.SetParent(Player.Instance.Hand);
        //transform.localPosition = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        rb.useGravity = true;
        collider.isTrigger = true;
        isGrabbed = true;

        startRotation = transform.localRotation;

        Color c = GetComponent<Renderer>().material.color;
        c.a = 0.5f;
        GetComponent<Renderer>().material.color = c;
    }

    void Update()
    {
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