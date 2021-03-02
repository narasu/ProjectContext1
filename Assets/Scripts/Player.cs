using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public enum KeyState {nothing, X, Y, Z};
    public KeyState keyState;
    Vector2 startingMousePos;
    Vector3 startRotation;
    float deltaX;
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

        keyState = KeyState.nothing;
    }

    private void SwitchToKey(KeyState keyStateToSwitchTo)
    {
        keyState = keyStateToSwitchTo;
        startingMousePos = Input.mousePosition;
        startRotation = inHand.gameObject.transform.localRotation.eulerAngles;
        Cursor.lockState = CursorLockMode.None;
    }

    private void SwitchToNothing()
    {
        keyState = KeyState.nothing;
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void Update()
    {
        //Input event for interacting with objects
        if (Input.GetMouseButtonDown(0) && keyState == KeyState.nothing)
        {
            InteractWithObject();
        }

        if (inHand!=null)
        {
            if(Input.GetKeyDown(KeyCode.X) && keyState != KeyState.X)
                SwitchToKey(KeyState.X);
            else if(Input.GetKeyDown(KeyCode.X) && keyState == KeyState.X)
                SwitchToNothing();

            if(Input.GetKeyDown(KeyCode.C) && keyState != KeyState.Y)
                SwitchToKey(KeyState.Y);
            else if(Input.GetKeyDown(KeyCode.C) && keyState == KeyState.Y)
                SwitchToNothing();

            if(Input.GetKeyDown(KeyCode.Z) && keyState != KeyState.Z)
                SwitchToKey(KeyState.Z);
            else if(Input.GetKeyDown(KeyCode.Z) && keyState == KeyState.Z)
                SwitchToNothing();

            if(Input.GetMouseButtonDown(0) && keyState != KeyState.nothing)
            {
                SwitchToNothing();
            }

            switch (keyState)
            {
                case KeyState.X:
                    inHand.gameObject.transform.Rotate(Vector3.forward, Input.mousePosition.x - deltaX, Space.Self);
                    deltaX = Input.mousePosition.x;
                    break;
                case KeyState.Y:
                    inHand.gameObject.transform.Rotate(Vector3.up, Input.mousePosition.x - deltaX, Space.Self);
                    deltaX = Input.mousePosition.x;
                    break;
                case KeyState.Z:
                    inHand.gameObject.transform.Rotate(Vector3.right, Input.mousePosition.x - deltaX, Space.Self);
                    deltaX = Input.mousePosition.x;
                    break;
            }

            // float xRollInput = Input.GetAxisRaw("X Roll");
            // float zRollInput = Input.GetAxisRaw("Z Roll");
            // //Quaternion nextRotation = Quaternion.Euler(transform.right);
            // var xRoll = Camera.main.transform.right * xRollInput * rotateSpeed * Time.deltaTime;
            // var zRoll = Camera.main.transform.forward * zRollInput * rotateSpeed * Time.deltaTime;
            // var totalRoll = xRoll + zRoll;
            // //Debug.Log(totalRoll);

            // inHand.gameObject.transform.Rotate(totalRoll, Space.World);

            if (Input.GetMouseButtonDown(1))
            {
                inHand.gameObject.transform.localRotation = Quaternion.Euler(Vector3.zero);
            }

            Transform t = inHand.gameObject.transform;
            Vector3 resizeScale = t.localScale + Vector3.one * Input.mouseScrollDelta.y * 0.1f;

            Vector3 newScale = new Vector3();
            newScale.x = Mathf.Clamp(resizeScale.x, 0.1f, 2f);
            newScale.y = Mathf.Clamp(resizeScale.y, 0.1f, 2f);
            newScale.z = Mathf.Clamp(resizeScale.z, 0.1f, 2f);

            inHand.gameObject.transform.localScale = newScale;
        }
    }
    public void InteractWithObject()
    {
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
        lookingAt = PlayerLook.Instance.GetTarget();
        if (lookingAt == null)
        {
            return;
        }

        lookingAt.GotoInteracting();
        inHand = lookingAt.GetComponent<Movable>();
        if (inHand != null)
        {
            inHand.Grab();
        }
    }

    private void ClearHand() => inHand = null;
}