using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    [SerializeField] Camera eventCamera;
    bool inventoryIsSpawned = false;
    Transform inventoryPos;
    PhotonView PV;
    void Start()
    {
        PV = GetComponent<PhotonView>();
        inventoryPos = transform.GetChild(2).transform;
        
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
            if (!inventoryIsSpawned)
            {
                GameObject c = Instantiate(inventory, inventoryPos.position, Quaternion.identity);
                c.GetComponent<WorldspaceCanvas>()?.Initialize(eventCamera);
                Debug.Log("spawned inventory");
                inventoryIsSpawned = true;
            }
            inventory.transform.position = inventoryPos.position;
            inventory.transform.LookAt(transform.position);

            int slopeInventoryPanelRotation = 10;
            inventory.transform.localRotation *= Quaternion.Euler(slopeInventoryPanelRotation, -180, 0);
            inventory.SetActive(true);
        }
    }
}
