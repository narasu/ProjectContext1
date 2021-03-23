using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject inventoryPrefab;
    GameObject inventory;
    [SerializeField] Camera eventCamera;
    bool inventoryIsSpawned = false;
    [SerializeField] Transform inventoryPos;
    PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        inventoryPos = transform.GetChild(2).transform;
        inventory = Instantiate(inventoryPrefab, inventoryPos.position, Quaternion.identity);
        inventory.GetComponent<WorldspaceCanvas>()?.Initialize(eventCamera);
        inventory.SetActive(false);
    }
    void Update()
    {
        if (!PV.IsMine)
        {
            return;
        }
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<IButton>() != null && Input.GetMouseButtonDown(0) && GetComponent<Player>().inHand == null)
            {
                hit.transform.gameObject.GetComponent<IButton>().Interact(this.transform);
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab) && GetComponent<Player>().inHand == null)
        {
            
            inventory.SetActive(true);
            inventory.transform.position = inventoryPos.position;
            inventory.transform.LookAt(transform.position);

            int slopeInventoryPanelRotation = 10;
            inventory.transform.localRotation *= Quaternion.Euler(slopeInventoryPanelRotation, -180, 0);
            
        }

    }
}
