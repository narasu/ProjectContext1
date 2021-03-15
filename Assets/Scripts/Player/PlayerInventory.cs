using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerInventory : MonoBehaviour
{
    [SerializeField] GameObject inventory;
    Transform inventoryPos;
    void Start()
    {
        inventoryPos = transform.GetChild(2).transform;
    }
    void Update()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.gameObject.GetComponent<Button>())
            {
                Debug.Log("test");
            }
        }
        if(Input.GetKeyDown(KeyCode.Tab) && Player.Instance.inHand == null)
        {
            inventory.transform.position = inventoryPos.position;
            inventory.transform.LookAt(transform.position);
            inventory.transform.localRotation *= Quaternion.Euler(20, -180, 0);
            inventory.SetActive(true);
        }
    }

    public void QuitInventory()
    {
        inventory.SetActive(false);
    }
}
