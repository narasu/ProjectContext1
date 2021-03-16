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
<<<<<<< HEAD
            //Transform headTrans = FindObjectOfType<Player>().transform.GetChild(1).transform;
            GameObject prefab = Instantiate(gameObjectToSpawnIn, Player.Instance.Hand.transform.position, Quaternion.identity);
            FindObjectOfType<Player>().InteractWithObject();
=======
            StartCoroutine(InteractWithDelay());
>>>>>>> c877d711cbdceac0e6a39d4ac22f03810285ce0f
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
