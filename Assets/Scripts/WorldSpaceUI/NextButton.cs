using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour, IButton
{
    PlayerInventory pi;
    void Start()
    {
        pi = FindObjectOfType<PlayerInventory>().GetComponent<PlayerInventory>();
    }
    public void Interact()
    {
        pi.inventoryPages[pi.currentPageIndex].SetActive(false);
        //Debug.Log(pi.currentPageIndex + "  " + pi.inventoryPages.Length);
        if(pi.currentPageIndex == pi.inventoryPages.Length - 1)
        {
            pi.currentPageIndex = 0;
        }
        else
        {
            pi.currentPageIndex++;
        }
        pi.inventoryPages[pi.currentPageIndex].SetActive(true);
    }
}
