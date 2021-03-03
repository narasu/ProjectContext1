using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformInHand : MonoBehaviour
{
    public enum KeyStates {nothing, X, Y, Z};
    [HideInInspector] public KeyStates keyState;
    Vector2 startingMousePos;
    Vector3 startRotation;
    float deltaX;
    Player player;
    void Start()
    {
        keyState = KeyStates.nothing;
        player = gameObject.GetComponent<Player>();
    }
    void Update()
    {
        if (player.inHand!=null)
        {
            if(Input.GetKeyDown(KeyCode.X) && keyState != KeyStates.X)
                SwitchToKey(KeyStates.X);
            else if(Input.GetKeyDown(KeyCode.X) && keyState == KeyStates.X)
                SwitchToNothing();

            if(Input.GetKeyDown(KeyCode.C) && keyState != KeyStates.Y)
                SwitchToKey(KeyStates.Y);
            else if(Input.GetKeyDown(KeyCode.C) && keyState == KeyStates.Y)
                SwitchToNothing();

            if(Input.GetKeyDown(KeyCode.Z) && keyState != KeyStates.Z)
                SwitchToKey(KeyStates.Z);
            else if(Input.GetKeyDown(KeyCode.Z) && keyState == KeyStates.Z)
                SwitchToNothing();

            switch (keyState)
            {
                case KeyStates.X:
                    player.inHand.gameObject.transform.Rotate(Vector3.forward, Input.mousePosition.x - deltaX, Space.Self);
                    deltaX = Input.mousePosition.x;
                    break;
                case KeyStates.Y:
                    player.inHand.gameObject.transform.Rotate(Vector3.up, Input.mousePosition.x - deltaX, Space.Self);
                    deltaX = Input.mousePosition.x;
                    break;
                case KeyStates.Z:
                    player.inHand.gameObject.transform.Rotate(Vector3.right, Input.mousePosition.x - deltaX, Space.Self);
                    deltaX = Input.mousePosition.x;
                    break;
            }
        }
    }

    void LateUpdate()
    {
        if(Input.GetMouseButtonDown(0) && keyState != KeyStates.nothing)
        {
            SwitchToNothing();
        }
    }

    private void SwitchToKey(KeyStates keyStateToSwitchTo)
    {
        // if(Input.GetMouseButtonDown(0) && keyState != KeyStates.nothing)
        // {
        //     keyState = KeyStates.nothing;
        // }
        keyState = keyStateToSwitchTo;
        startingMousePos = Input.mousePosition;
        startRotation = player.inHand.gameObject.transform.localRotation.eulerAngles;
        Cursor.lockState = CursorLockMode.None;

        GameObject gizmo3D = player.inHand.transform.GetChild(0).gameObject;

        for (int i = 0; i < 3; i++)
        {
            gizmo3D.transform.GetChild(i).gameObject.SetActive(false);
        }

        if(keyStateToSwitchTo == KeyStates.X)
        {
            gizmo3D.transform.GetChild(0).gameObject.SetActive(true);
            return;
        }
        if(keyStateToSwitchTo == KeyStates.Z)
        {
            gizmo3D.transform.GetChild(1).gameObject.SetActive(true);
            return;
        }
        if(keyStateToSwitchTo == KeyStates.Y)
        {
            gizmo3D.transform.GetChild(2).gameObject.SetActive(true);
            return;
        }
    }

    private void SwitchToNothing()
    {
        keyState = KeyStates.nothing;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject gizmo3D = player.inHand.transform.GetChild(0).gameObject;

        for (int i = 0; i < 3; i++)
        {
            gizmo3D.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

