using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLook : MonoBehaviour
{
    
    private static PlayerLook instance;
    public static PlayerLook Instance
    {
        get
        {
            return instance;
        }
    }
#pragma warning disable 0649
    public Vector2 lookVector;
    [SerializeField] private Transform playerBody;
    [SerializeField] [Range(0, 20)] private float raycastDistance = 10f;
    [SerializeField] [Range(0, 150)] private float mouseSensitivity = 100f;
#pragma warning restore 0649

    [HideInInspector] public Camera camera;
    private float xAxisClamp;

    private Interactable target;
    private Interactable lastTarget;
    private PlayerTransformInHand playerTransformInHand;

    private void Awake()
    {
        instance = this;
        camera = GetComponent<Camera>();
        playerTransformInHand = FindObjectOfType<PlayerTransformInHand>();
        LockCursor();
    }

    private void Update()
    {
        if(playerTransformInHand.keyState == PlayerTransformInHand.KeyStates.nothing)
            CameraRotation();

        lastTarget = GetTarget();
        RaycastHit hit;
        //Cast a ray and scan for an Interactable target
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance)) 
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Interactable i = hit.transform.GetComponent<Interactable>();
            
            if(i!=null && lastTarget != i)
            {
                if (i.isActiveAndEnabled)
                {
                    i.Highlight();
                    SetTarget(i);
                }
                
            }
            else if (lastTarget!=i)
            {
                SetTarget(null);
            }
        }
    }

    private void CameraRotation()
    {
        //set mouse movement values
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        xAxisClamp += mouseY;

        //clamp rotation when looking up
        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(270.0f);
        }

        //clamp rotation when looking down
        if (xAxisClamp < -80.0f)
        {
            xAxisClamp = -80.0f;
            mouseY = 0.0f;
            ClampXAxisRotationToValue(80.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampXAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }

    //set target for the player to interact with
    private void SetTarget(Interactable i)
    {
        target = i;
    }

    public Interactable GetTarget()
    {
        return target;
    }

    private void LockCursor()
    {
        //hides and locks cursor
        Cursor.lockState = CursorLockMode.Locked;
    }
}