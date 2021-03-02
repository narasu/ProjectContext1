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
        Debug.Log("test");
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
                    Debug.Log("changed hit");
                }

                cachedOutline = hit.transform.gameObject.GetComponent<Outline>();
                cachedOutline.enabled = true;
                Debug.Log("hit");
            }
            
        } 
        else 
        {
            Debug.DrawRay(Camera.main.transform.position, Camera.main.transform.forward * 1000, Color.white);
            cachedOutline.enabled = false;
            Debug.Log("not hit");
        }
    }
}
