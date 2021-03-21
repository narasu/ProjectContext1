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
        Debug.Log(lookingAt + "  " + !editMatActive);
        if(lookingAt != null && !editMatActive || Player.Instance.inHand != null && !editMatActive)
        {
            ActiveEditMat();
        }

        else if(editMatActive && Input.GetMouseButtonDown(1))
        {
            DeactivateEditMat();
        }
    }

    void ActiveEditMat()
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
                if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.GetComponent<IBuildingBlock>().ActiveEditMaterial();
                    return;
                }
            }
            Player.Instance.inHand.GetComponent<IBuildingBlock>().ActiveEditMaterial();
        }
    }

    void DeactivateEditMat()
    {
        editMatActive = false;
        editMatUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        if(Player.Instance.inHand == null)
        {
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("BuildingBlock");
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                hit.transform.GetComponent<IBuildingBlock>().DeactiveEditMaterial();
                return;
            }
        }
        Player.Instance.inHand.GetComponent<IBuildingBlock>().DeactiveEditMaterial();
    }
}
