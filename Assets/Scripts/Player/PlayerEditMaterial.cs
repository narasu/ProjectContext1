using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerEditMaterial : MonoBehaviour
{
    public bool editMatActive;
    [SerializeField] GameObject editMatUI;
    private Interactable lookingAt;
    Player player;
    [SerializeField] PlayerLook playerLook;

    private void Awake()
    {
        //localPlayerObject = PhotonNetwork.LocalPlayer.TagObject as GameObject;
        //Debug.Log(PhotonNetwork.LocalPlayer.TagObject);
        player = GetComponent<Player>();
        
    }
    void Update()
    {
        lookingAt = playerLook.GetTarget();
        if(lookingAt != null && !editMatActive || player.inHand != null && !editMatActive)
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
            if(player.inHand == null)
            {
                RaycastHit hit;
                int layerMask = LayerMask.GetMask("BuildingBlock");
                if (Physics.Raycast(playerLook.transform.position, playerLook.transform.forward, out hit, Mathf.Infinity, layerMask))
                {
                    hit.transform.GetComponent<IBuildingBlock>().ActiveEditMaterial();
                    return;
                }
            }
            player.inHand.GetComponent<IBuildingBlock>().ActiveEditMaterial();
        }
    }

    void DeactivateEditMat()
    {
        editMatActive = false;
        editMatUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        if(player.inHand == null)
        {
            RaycastHit hit;
            int layerMask = LayerMask.GetMask("BuildingBlock");
            if (Physics.Raycast(playerLook.transform.position, playerLook.transform.forward, out hit, Mathf.Infinity, layerMask))
            {
                hit.transform.GetComponent<IBuildingBlock>().DeactiveEditMaterial();
                return;
            }
        }
        player.inHand.GetComponent<IBuildingBlock>().DeactiveEditMaterial();
    }
}
