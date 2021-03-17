using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    public GameObject[] inventoryPages;
    public int currentPageIndex;
    Transform inventoryPos;
    void Start()
    {
        inventoryPos = transform.GetChild(2).transform;
        for (int i = 0; i < inventoryPages.Length; i++)
        {
            inventoryPages[i].SetActive(false);
        }
        inventoryPages[0].SetActive(true);
    }
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<IButton>() != null && Input.GetMouseButtonDown(0) && Player.Instance.inHand == null)
            {
                hit.transform.gameObject.GetComponent<IButton>().Interact();
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab) && Player.Instance.inHand == null)
        {
            inventory.transform.position = inventoryPos.position;
            inventory.transform.LookAt(transform.position);

            int slopeInventoryPanelRotation = 10;
            inventory.transform.localRotation *= Quaternion.Euler(slopeInventoryPanelRotation, -180, 0);
            inventory.SetActive(true);
        }
    }
}
