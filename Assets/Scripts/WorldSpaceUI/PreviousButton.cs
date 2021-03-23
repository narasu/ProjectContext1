using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousButton : MonoBehaviour, IButton
{
    public WorldspaceCanvas inventory;
    void Start()
    {
    }
    public void Interact(Transform player)
    {
        inventory.inventoryPages[inventory.currentPageIndex].SetActive(false);
        if(inventory.currentPageIndex == 0)
        {
            inventory.currentPageIndex = inventory.inventoryPages.Length - 1;
        }
        else
        {
            inventory.currentPageIndex--;
        }
        inventory.inventoryPages[inventory.currentPageIndex].SetActive(true);
    }
}
