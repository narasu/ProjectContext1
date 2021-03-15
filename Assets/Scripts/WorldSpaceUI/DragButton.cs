using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragButton : MonoBehaviour, IButton
{
    [SerializeField] GameObject gameObjectToSpawnIn;
    bool hasClicked;
    public void Interact()
    {
        if(Player.Instance.inHand == null)
        {
            //Transform headTrans = FindObjectOfType<Player>().transform.GetChild(1).transform;
            GameObject prefab = Instantiate(gameObjectToSpawnIn, Player.Instance.Hand.transform.position, Quaternion.identity);
            //FindObjectOfType<Player>().InteractWithObject();
        }
    }
}
