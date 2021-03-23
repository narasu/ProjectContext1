using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButton : MonoBehaviour, IButton
{
    public GameObject canvas;
    public void Interact(Transform player)
    {
        canvas.SetActive(false);
    }
}
