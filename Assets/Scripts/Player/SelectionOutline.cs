using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionOutline : MonoBehaviour
{
    Outline cachedOutline;
    Player player;
    void Start() 
    {
        player = gameObject.GetComponent<Player>();
    }
    void Update()
    {
        int layerMask = 1 << 6;
        
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, Mathf.Infinity, layerMask)) 
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * hit.distance, Color.yellow);
            if(player.inHand == null)
            {
                if(cachedOutline != hit.transform.gameObject.GetComponent<Outline>() && cachedOutline != null)
                {
                    cachedOutline.enabled = false;
                }

                cachedOutline = hit.transform.gameObject.GetComponent<Outline>();
                cachedOutline.enabled = true;
            }
        } 
        else 
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000, Color.white);

            if(cachedOutline != null)
                cachedOutline.enabled = false;
        }
    }
}
