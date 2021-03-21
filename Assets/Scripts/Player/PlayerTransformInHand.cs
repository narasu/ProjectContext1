using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformInHand : MonoBehaviour
{
    public enum KeyStates {nothing, rotateSelection, resizeSelection, rotateX, rotateY, rotateZ, resizeX, resizeY, resizeZ};
    public KeyStates keyState;
    [SerializeField] GameObject head;
    [SerializeField] GameObject gizmo;
    [SerializeField] float scaleDampener;
    [SerializeField] float moveDampener = 0.05f;
    Quaternion startRotation;
    Vector3 startScale;
    float lastMousePos;
    Player player;
    GameObject inHand;
    void Start()
    {
        keyState = KeyStates.nothing;
        player = gameObject.GetComponent<Player>();
    }
    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0)){
                if(keyState == KeyStates.resizeSelection || keyState == KeyStates.rotateSelection){
                    SwitchToNothing();
                }
            }
            if(Input.GetMouseButtonDown(0)){
                if(keyState == KeyStates.resizeX || keyState == KeyStates.resizeY || keyState == KeyStates.resizeZ){
                    SwitchToSelection(KeyStates.resizeSelection);
                }
            }
    }
    void Update()
    {
        if (player.inHand!=null)
        {
            inHand = player.inHand.gameObject;
            Movable inHandMovable = inHand.GetComponent<Movable>();
            // Vector3 moveAlongVector = (inHand.transform.position - head.transform.position) * Input.mouseScrollDelta.y * moveDampener;
            // inHand.transform.position += moveAlongVector;
            float resizeScalerX = inHand.transform.localScale.x + inHandMovable.resizeScalerX * Input.mouseScrollDelta.y;
            float resizeScalerY = inHand.transform.localScale.y + inHandMovable.resizeScalerY * Input.mouseScrollDelta.y;
            float resizeScalerZ = inHand.transform.localScale.z + inHandMovable.resizeScalerZ * Input.mouseScrollDelta.y;

            Vector3 resizeScale = new Vector3(resizeScalerX, resizeScalerY, resizeScalerZ);

            inHand.transform.localScale = resizeScale;

            if(Input.GetKeyDown(KeyCode.R)){
                SwitchToSelection(KeyStates.rotateSelection);
            }
            if(Input.GetKeyDown(KeyCode.F)){
                SwitchToSelection(KeyStates.resizeSelection);
            }

            
            

            if(keyState != KeyStates.nothing || keyState != KeyStates.resizeSelection || keyState != KeyStates.rotateSelection){
                if(gizmo.transform.position != inHand.transform.position){
                    gizmo.transform.position = inHand.transform.position;
                }
                // if(gizmo.transform.rotation != inHand.transform.rotation){
                //     gizmo.transform.rotation = inHand.transform.rotation;
                // }
            }

            if(keyState == KeyStates.rotateSelection || keyState == KeyStates.rotateX || keyState == KeyStates.rotateZ || keyState == KeyStates.rotateY){
                if(Input.GetKeyDown(KeyCode.X) && keyState != KeyStates.rotateX){
                    SwitchToKey(KeyStates.rotateX);
                }
                else if(Input.GetKeyDown(KeyCode.X) && keyState == KeyStates.rotateX){
                    SwitchToNothing();
                }

                if(Input.GetKeyDown(KeyCode.C) && keyState != KeyStates.rotateY){
                    SwitchToKey(KeyStates.rotateY);
                }
                else if(Input.GetKeyDown(KeyCode.C) && keyState == KeyStates.rotateY){
                    SwitchToNothing();
                }

                if(Input.GetKeyDown(KeyCode.Z) && keyState != KeyStates.rotateZ){
                    SwitchToKey(KeyStates.rotateZ);
                }
                else if(Input.GetKeyDown(KeyCode.Z) && keyState == KeyStates.rotateZ){
                    SwitchToNothing();
                }
            }

            if(keyState == KeyStates.resizeSelection || keyState == KeyStates.resizeX || keyState == KeyStates.resizeZ || keyState == KeyStates.resizeY){
                if(Input.GetKeyDown(KeyCode.X) && keyState != KeyStates.resizeX){
                    SwitchToKey(KeyStates.resizeX);
                }
                else if(Input.GetKeyDown(KeyCode.X) && keyState == KeyStates.resizeX){
                    SwitchToNothing();
                }

                if(Input.GetKeyDown(KeyCode.C) && keyState != KeyStates.resizeY){
                    SwitchToKey(KeyStates.resizeY);
                }
                else if(Input.GetKeyDown(KeyCode.C) && keyState == KeyStates.resizeY){
                    SwitchToNothing();
                }

                if(Input.GetKeyDown(KeyCode.Z) && keyState != KeyStates.resizeZ){
                    SwitchToKey(KeyStates.resizeZ);
                }
                else if(Input.GetKeyDown(KeyCode.Z) && keyState == KeyStates.resizeZ){
                    SwitchToNothing();
                }
            }

            switch (keyState)
            {
                case KeyStates.rotateX:
                    inHand.transform.localRotation = startRotation * Quaternion.Euler(0, 0, Input.mousePosition.x - lastMousePos - Screen.width / 2);
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.rotateSelection);
                    }
                    break;
                case KeyStates.rotateY:
                    inHand.transform.localRotation = startRotation * Quaternion.Euler(0, Input.mousePosition.x - lastMousePos - Screen.width / 2, 0);
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.rotateSelection);
                    }
                    break;
                case KeyStates.rotateZ:
                    inHand.transform.localRotation = startRotation * Quaternion.Euler(Input.mousePosition.x - lastMousePos - Screen.width / 2, 0, 0);
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.rotateSelection);
                    }
                    break;

                case KeyStates.resizeX:
                    inHand.transform.localScale = new Vector3(0, 0, (Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener * (inHandMovable.resizeScalerZ * 10)) + startScale;
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.resizeSelection);
                    }
                    break;
                case KeyStates.resizeY:
                    inHand.transform.localScale = new Vector3(0, (Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener * (inHandMovable.resizeScalerY * 10), 0) + startScale;
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.resizeSelection);
                    }
                    break;
                case KeyStates.resizeZ:
                    inHand.transform.localScale = new Vector3((Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener * (inHandMovable.resizeScalerX * 10), 0, 0) + startScale;
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.resizeSelection);
                    }
                    break;
            }
        }
    }

    private void SwitchToKey(KeyStates keyStateToSwitchTo)
    {
        startRotation = inHand.transform.localRotation;
        startScale = inHand.transform.localScale;
        lastMousePos = Input.mousePosition.x - (Screen.width / 2);
        gizmo.transform.rotation = inHand.transform.rotation;

        keyState = keyStateToSwitchTo;
        Cursor.lockState = CursorLockMode.None;

        gizmo.SetActive(true);

        for (int i = 0; i < 3; i++)
        {
            gizmo.transform.GetChild(i).gameObject.SetActive(false);
        }

        if(keyStateToSwitchTo == KeyStates.rotateX || keyStateToSwitchTo == KeyStates.resizeX)
        {
            gizmo.transform.GetChild(0).gameObject.SetActive(true);
            return;
        }
        if(keyStateToSwitchTo == KeyStates.rotateZ || keyStateToSwitchTo == KeyStates.resizeZ)
        {
            gizmo.transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        if(keyStateToSwitchTo == KeyStates.rotateY || keyStateToSwitchTo == KeyStates.resizeY)
        {
            gizmo.transform.GetChild(2).gameObject.SetActive(true);
            return;
        }
    }

    private void SwitchToSelection(KeyStates keyStateToSwitchTo)
    {
        keyState = keyStateToSwitchTo;
        Cursor.lockState = CursorLockMode.Locked;
        gizmo.transform.rotation = inHand.transform.rotation;
        gizmo.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            gizmo.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void SwitchToNothing()
    {
        keyState = KeyStates.nothing;
        Cursor.lockState = CursorLockMode.Locked;

        gizmo.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            gizmo.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

