using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private static Player instance;
    public static Player Instance
    {
        get
        {
            return instance;
        }
    }
    
    /*    Interactions    */
    //Object the player is looking at
    private Interactable lookingAt;
    private PlayerTransformInHand playerTransformInHand;
    //Object of type movable that the player is holding
    [HideInInspector] public Movable inHand;
    [SerializeField] private Transform hand;
    public Transform Hand
    {
        get { return hand; }
    }
    [SerializeField] private float rotateSpeed;

    private void Awake()
    {
        //Initialize variables;
        instance = this;

        playerTransformInHand = gameObject.GetComponent<PlayerTransformInHand>();
    }
    private void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if (Input.GetMouseButtonDown(0) && playerTransformInHand.keyState == PlayerTransformInHand.KeyStates.nothing && hit.transform.gameObject.GetComponent<IButton>() == null
                && !FindObjectOfType<PlayerEditMaterial>().editMatActive) 
            {
                InteractWithObject();
            }
        }
        else if(Input.GetMouseButtonDown(0) && playerTransformInHand.keyState == PlayerTransformInHand.KeyStates.nothing && !FindObjectOfType<PlayerEditMaterial>().editMatActive)
        {
            InteractWithObject();
        }
    }
    public void InteractWithObject()
    {
        if (inHand != null)
        {
            // if (inHand.GetComponent<Rigidbody>()==null) //check if an object got removed but failed to clear
            // {
            //     ClearHand();
            //     return;
            // }
            Debug.Log("drop3");
            inHand.GetComponent<IBuildingBlock>().Drop();
            ClearHand();
            return;
        }
        lookingAt = PlayerLook.Instance.GetTarget();
        if (lookingAt == null)
        {
            return;
        }

        lookingAt.GotoInteracting();
        inHand = lookingAt.GetComponent<Movable>();
        if (inHand != null)
        {
            inHand.GetComponent<IBuildingBlock>().Grab();
        }
    }

    private void ClearHand() => inHand = null;
}