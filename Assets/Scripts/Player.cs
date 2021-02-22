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
    //Object of type movable that the player is holding
    [HideInInspector] public Movable inHand;

    /*    Character Movement    */
    private CharacterController charController;
    [SerializeField] private float movementSpeed = 8.0f;
    [HideInInspector] public Vector2 walkVector;
    [HideInInspector] public float forwardInput, horizInput;
    private Vector3 forwardMovement, rightMovement, movement;
	

    private void Awake()
    {
        //Initialize variables;
        instance = this;
        charController = GetComponent<CharacterController>();
    }

    private void Start()
    {
		
    }

    private void Update()
    {
        //update movement vectors based on player input
            forwardInput = Input.GetAxisRaw("Vertical");
            horizInput = Input.GetAxisRaw("Horizontal");


            walkVector = new Vector2(horizInput, forwardInput);


            //Input event for interacting with objects
            if (Input.GetMouseButtonDown(0))
            {
                Interact();
            }
        Walk();
    }
	
    //player movement
    public void Walk()
    {
        forwardMovement = transform.forward * walkVector.y;
        rightMovement = transform.right * walkVector.x;

        movement = Vector3.Normalize(forwardMovement + rightMovement) * movementSpeed;
        charController.SimpleMove(movement);
    }

    //Interact with objects
    public void Interact()
    {
        //does the player have something in hand? if so, drop. 
        if (inHand != null)
        {
            if (inHand.GetComponent<Rigidbody>()==null) //check if an object got removed but failed to clear
            {
                ClearHand();
                return;
            }
            inHand.Drop();
            ClearHand();
            return;
        }

        // is the player looking at a valid Interactable target?
        lookingAt = PlayerLook.Instance.GetTarget();
        if (lookingAt == null)
            return;

        //set target to interacting state
        lookingAt.GotoInteracting();

        //is the object movable? if so, grab
        inHand = lookingAt.GetComponent<Movable>();
        if (inHand != null)
        {
            inHand.Grab();
        }
    }

    public void ClearHand()
    {
        inHand = null;
    }
}