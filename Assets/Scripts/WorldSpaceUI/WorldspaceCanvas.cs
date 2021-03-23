using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldspaceCanvas : MonoBehaviour
{
    public GameObject[] inventoryPages;
    public int currentPageIndex;

    public void Initialize(Camera playerCamera)
    {
        GetComponent<Canvas>().worldCamera = playerCamera;
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < inventoryPages.Length; i++)
        {
            inventoryPages[i].SetActive(false);
        }
        inventoryPages[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
