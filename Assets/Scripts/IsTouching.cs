using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsTouching : MonoBehaviour
{
    bool alreadySelecting;
    Player player;

    void Start()
    {
        player = FindObjectOfType<Player>();
    }
    void OnTriggerStay(Collider other)
    {
        Debug.Log(other);
        //Layer 6 is BuildingBlock layer
        if(other.gameObject.layer == 6 && alreadySelecting == false && player.inHand == null){
            other.gameObject.GetComponent<Outline>().enabled = true;
            alreadySelecting = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        //Layer 6 is BuildingBlock layer
        if(other.gameObject.layer == 6){
            other.gameObject.GetComponent<Outline>().enabled = false;
            alreadySelecting = false;
        }
    }
}
