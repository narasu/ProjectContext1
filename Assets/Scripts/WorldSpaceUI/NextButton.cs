using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour, IButton
{
    public WorldspaceCanvas inventory;
    void Start()
    {
        //pi = FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
    }
    public void Interact(Transform player)
    {
        inventory.inventoryPages[inventory.currentPageIndex].SetActive(false);
        //Debug.Log(pi.currentPageIndex + "  " + pi.inventoryPages.Length);
        if(inventory.currentPageIndex == inventory.inventoryPages.Length - 1)
        {
            inventory.currentPageIndex = 0;
        }
        else
        {
            inventory.currentPageIndex++;
        }
        inventory.inventoryPages[inventory.currentPageIndex].SetActive(true);
    }
}
