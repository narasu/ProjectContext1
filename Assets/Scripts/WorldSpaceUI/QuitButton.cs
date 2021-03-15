using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour, IButton
{
    public void Interact()
    {
        GameObject.FindGameObjectWithTag("Inventory").SetActive(false);
    }
}
