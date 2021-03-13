using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransformInHand : MonoBehaviour
{
    public enum KeyStates {nothing, X, Y, Z};
    [HideInInspector] public KeyStates keyState;
    [SerializeField] GameObject head;
    public float test;
    Quaternion startRotation;
    Vector2 startingMousePos;
    float deltaX;
    float lastMousePos;
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
            float moveDampener = 0.05f;
            Vector3 moveAlongVector = (player.inHand.gameObject.transform.position - head.transform.position) * Input.mouseScrollDelta.y * moveDampener;
            player.inHand.gameObject.transform.position += moveAlongVector;

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
                    player.inHand.gameObject.transform.localRotation = startRotation * Quaternion.Euler(0, 0, Input.mousePosition.x - Screen.width / 2);
                    deltaX = Input.mousePosition.x;
                    break;
                case KeyStates.Y:
                    //player.inHand.gameObject.transform.Rotate(Vector3.up, Input.mousePosition.x - deltaX, Space.Self);
                    player.inHand.gameObject.transform.localRotation = startRotation * Quaternion.Euler(0, Input.mousePosition.x - Screen.width / 2, 0);
                    deltaX = Input.mousePosition.x;
                    break;
                case KeyStates.Z:
                    player.inHand.gameObject.transform.localRotation = startRotation * Quaternion.Euler(Input.mousePosition.x - Screen.width / 2, 0, 0);
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
        startRotation = player.inHand.gameObject.transform.localRotation;
        Debug.Log(startRotation);
        player.inHand.GetComponent<Movable>().unchangedRotation = false;
        keyState = keyStateToSwitchTo;
        Cursor.lockState = CursorLockMode.None;
        //Cursor.visible = true;

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
        player.inHand.GetComponent<Movable>().unchangedRotation = true;
        keyState = KeyStates.nothing;
        //ursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        GameObject gizmo3D = player.inHand.transform.GetChild(0).gameObject;

        for (int i = 0; i < 3; i++)
        {
            gizmo3D.transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}

