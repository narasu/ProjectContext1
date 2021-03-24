using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class DragButton : MonoBehaviour, IButton
{
    [SerializeField] string gameObjectToSpawnIn;
    bool hasClicked;
    Player player;
    public void Interact(Transform player)
    {
        this.player = player.GetComponent<Player>();
        if(this.player.inHand == null)
        {
            StartCoroutine(InteractWithDelay());
        }
    }

    IEnumerator InteractWithDelay()
    {
        //Transform headTrans = FindObjectOfType<Player>().transform.GetChild(1).transform;
        GameObject o = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs/BuildingBlocks", gameObjectToSpawnIn), player.Hand.transform.position, Quaternion.identity);
        //o.GetComponent<Movable>().PV.TransferOwnership(0);

        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        player.InteractWithObject();
    }
}
