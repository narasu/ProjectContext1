using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreviousButton : MonoBehaviour, IButton
{
    PlayerInventory pi;
    void Start()
    {
        pi = FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
    }
    public void Interact()
    {
        pi.inventoryPages[pi.currentPageIndex].SetActive(false);
        if(pi.currentPageIndex == 0)
        {
            pi.currentPageIndex = pi.inventoryPages.Length - 1;
        }
        else
        {
            pi.currentPageIndex--;
        }
        pi.inventoryPages[pi.currentPageIndex].SetActive(true);
    }
}
