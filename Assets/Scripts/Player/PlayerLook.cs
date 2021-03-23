using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerLook : MonoBehaviour
{
    PhotonView PV;

    

#pragma warning disable 0649
    public Vector2 lookVector;
    [SerializeField] private Transform playerBody;
    [SerializeField] [Range(0, 20)] private float raycastDistance = 10f;
    [SerializeField] [Range(0, 150)] private float mouseSensitivity = 100f;
#pragma warning restore 0649

    [HideInInspector] public Camera camera;
    private float xAxisClamp;

    [SerializeField] Player owner;
    private Interactable target;
    public Interactable lastTarget;
    private PlayerTransformInHand playerTransformInHand;
    private PlayerEditMaterial playerEditMaterial;

    

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        if (!PV.IsMine)
        {
            return;
        }
        camera = GetComponent<Camera>();
        playerTransformInHand = FindObjectOfType<PlayerTransformInHand>();
        playerEditMaterial = FindObjectOfType<PlayerEditMaterial>();
        
        LockCursor();
    }

    private void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        if(playerTransformInHand.keyState == PlayerTransformInHand.KeyStates.nothing && !playerEditMaterial.editMatActive)
            CameraRotation();

        ScanForTargets();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
    void ScanForTargets()
    {
        lastTarget = target;
        RaycastHit hit;
        int layerMask = LayerMask.GetMask("BuildingBlock");
        //Cast a ray and scan for an Interactable target
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, raycastDistance, layerMask))
        {
            //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            Interactable interactable = hit.transform.GetComponent<Interactable>();

            if (interactable == null)
            {
                SetTarget(null);
                return;
            }

            if (interactable != null/* && lastTarget != interactable*/)
            {
                if (interactable.isActiveAndEnabled)
                {
                    if (owner.inHand == null)
                    {
                        interactable.Highlight();
                        SetTarget(interactable);
                    }
                }

            }
            else if (lastTarget != interactable)
            {
                SetTarget(null);
            }
        }
        else
        {
            SetTarget(null);
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
        if (target!=null)
        {
            target.isTargeted = false;
        }
        
        target = i;
        if (i!=null)
        {
            i.isTargeted = true;
        }
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