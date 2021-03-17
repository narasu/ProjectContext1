using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEditMaterial : MonoBehaviour
{
    public bool editMatActive;
    [SerializeField] GameObject editMatUI;
    private Interactable lookingAt;
    void Update()
    {
        lookingAt = PlayerLook.Instance.GetTarget();
        if(lookingAt != null && !editMatActive)
        {
            if (Input.GetMouseButtonDown(1))
            {
                editMatUI.SetActive(true);
                editMatActive = true;
                Cursor.lockState = CursorLockMode.None;
                if(Player.Instance.inHand == null)
                {
                    RaycastHit hit;
                    int layerMask = LayerMask.GetMask("BuildingBlock");
                    if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
                    {
                        hit.transform.GetComponent<Movable>().ActiveEditMaterial();
                        return;
                    }
                }
                Player.Instance.inHand.GetComponent<Movable>().ActiveEditMaterial();
            }
        }
        else if(editMatActive && Input.GetMouseButtonDown(1))
        {
            editMatActive = false;
            editMatUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            if(Player.Instance.inHand == null)
            {
                RaycastHit hit;
                int layerMask = LayerMask.GetMask("BuildingBlock");
                if (Physics.Raycast(transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.GetComponent<Movable>().DeactiveEditMaterial();
                    return;
                }
            }
            Player.Instance.inHand.GetComponent<Movable>().DeactiveEditMaterial();
        }
    }
    void LateUpdate()
    {
    }
}
