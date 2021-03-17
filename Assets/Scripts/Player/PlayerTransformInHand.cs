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
            // Vector3 moveAlongVector = (player.inHand.gameObject.transform.position - head.transform.position) * Input.mouseScrollDelta.y * moveDampener;
            // player.inHand.gameObject.transform.position += moveAlongVector;

            Vector3 resizeScale = player.inHand.gameObject.transform.localScale + Vector3.one * Input.mouseScrollDelta.y * 0.1f;

            Vector3 newScale = new Vector3();
            newScale.x = Mathf.Clamp(resizeScale.x, 0.1f, 20f);
            newScale.y = Mathf.Clamp(resizeScale.y, 0.1f, 20f);
            newScale.z = Mathf.Clamp(resizeScale.z, 0.1f, 20f);

            player.inHand.gameObject.transform.localScale = newScale;

            if(Input.GetKeyDown(KeyCode.R)){
                SwitchToSelection(KeyStates.rotateSelection);
            }
            if(Input.GetKeyDown(KeyCode.F)){
                SwitchToSelection(KeyStates.resizeSelection);
            }

            
            

            if(keyState != KeyStates.nothing || keyState != KeyStates.resizeSelection || keyState != KeyStates.rotateSelection){
                if(gizmo.transform.position != player.inHand.gameObject.transform.position){
                    gizmo.transform.position = player.inHand.gameObject.transform.position;
                }
                // if(gizmo.transform.rotation != player.inHand.gameObject.transform.rotation){
                //     gizmo.transform.rotation = player.inHand.gameObject.transform.rotation;
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
                    player.inHand.gameObject.transform.localRotation = startRotation * Quaternion.Euler(0, 0, Input.mousePosition.x - lastMousePos - Screen.width / 2);
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.rotateSelection);
                    }
                    break;
                case KeyStates.rotateY:
                    player.inHand.gameObject.transform.localRotation = startRotation * Quaternion.Euler(0, Input.mousePosition.x - lastMousePos - Screen.width / 2, 0);
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.rotateSelection);
                    }
                    break;
                case KeyStates.rotateZ:
                    player.inHand.gameObject.transform.localRotation = startRotation * Quaternion.Euler(Input.mousePosition.x - lastMousePos - Screen.width / 2, 0, 0);
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.rotateSelection);
                    }
                    break;

                case KeyStates.resizeX:
                    player.inHand.gameObject.transform.localScale = new Vector3(0, 0, (Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener) + startScale;
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.resizeSelection);
                    }
                    break;
                case KeyStates.resizeY:
                    player.inHand.gameObject.transform.localScale = new Vector3(0, (Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener, 0) + startScale;
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.resizeSelection);
                    }
                    break;
                case KeyStates.resizeZ:
                    //clampedScale = Mathf.Clamp((Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener, 0f, 100f);
                    player.inHand.gameObject.transform.localScale = new Vector3((Input.mousePosition.x - lastMousePos - Screen.width / 2) * scaleDampener, 0, 0) + startScale;
                    if(Input.GetMouseButtonDown(0)){
                        SwitchToSelection(KeyStates.resizeSelection);
                    }
                    break;
            }
        }
    }

    private void SwitchToKey(KeyStates keyStateToSwitchTo)
    {
        startRotation = player.inHand.gameObject.transform.localRotation;
        startScale = player.inHand.gameObject.transform.localScale;
        lastMousePos = Input.mousePosition.x - (Screen.width / 2);
        gizmo.transform.rotation = player.inHand.gameObject.transform.rotation;

        keyState = keyStateToSwitchTo;
        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

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
        gizmo.transform.rotation = player.inHand.gameObject.transform.rotation;
        gizmo.SetActive(true);
        for (int i = 0; i < 3; i++)
        {
            gizmo.transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    private void SwitchToNothing()
    {
        keyState = KeyStates.nothing;
        //ursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        gizmo.SetActive(false);

        for (int i = 0; i < 3; i++)
        {
            gizmo.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

