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
            StartCoroutine(InteractWithDelay());
        }
    }

    IEnumerator InteractWithDelay()
    {
        //Transform headTrans = FindObjectOfType<Player>().transform.GetChild(1).transform;
        GameObject prefab = Instantiate(gameObjectToSpawnIn, Player.Instance.Hand.transform.position, Quaternion.identity);
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        Debug.Log(PlayerLook.Instance.GetTarget());
        FindObjectOfType<Player>().InteractWithObject();
    }
}
