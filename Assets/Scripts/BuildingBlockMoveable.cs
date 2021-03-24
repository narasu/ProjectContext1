using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Photon.Pun;

public class BuildingBlockMoveable : Movable, IBuildingBlock
{
    [SerializeField] Color selectionOutline = new Color32(255, 213, 143, 255);
    [SerializeField] Color grabOutline = new Color32(194, 238, 102, 255);
    Rigidbody rb;
    Collider collider;
    Material material;

    

    protected override void Awake()
    {
        base.Awake();
        rb = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>(); 
        material = renderer.material;

        resizeScalerX = transform.localScale.x / 10;
        resizeScalerY = transform.localScale.y / 10;
        resizeScalerZ = transform.localScale.z / 10;

        if(SetColorOnStart)
        {
            renderer.material.color = Color;
        }
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    Destroy(rb);
        //}
    }
    public override void Grab(Transform player)
    {
        
        base.Grab(player);
        collider.isTrigger = true;
        gameObject.GetComponent<Outline>().enabled = true;
        gameObject.GetComponent<Outline>().OutlineColor = grabOutline;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
        //if (PhotonNetwork.IsMasterClient)
        //{
            
        //}
    }

    public override void Drop()
    {
        //if (!PhotonNetwork.IsMasterClient)
        //{
        //    return;
        //}
        base.Drop();
        collider.isTrigger = false;
        gameObject.GetComponent<Outline>().OutlineColor = selectionOutline;
    }

    public override void HighlightInteractionOn()
    {
        gameObject.GetComponent<Outline>().enabled = true;
    }
    public override void HighlightInteractionOff()
    {
        gameObject.GetComponent<Outline>().enabled = false;
    }
    UnityAction<Color> a;
    public override void ActiveEditMaterial()
    {

        picker = FindObjectOfType<HSVPicker.ColorPicker>();
        a = color => SetColor(color);
        picker.onValueChanged.AddListener(a);

        //material.color = picker.CurrentColor;
    }
    public override void DeactiveEditMaterial()
    {
        picker.onValueChanged.RemoveListener(a);
    }

    public void SetColor(Color color)
    {
        //material.color = color;
        Color = color;
        PV.RPC("RPC_SendColor", RpcTarget.AllBuffered, null);
    }
    private void OnDisable()
    {
        PV.RPC("RPC_Disable", RpcTarget.AllBuffered, null);
    }
    [PunRPC]
    public void RPC_SendColor()
    {
        material.color = Color;
        //Color = color;
    }

    [PunRPC]
    public void RPC_Disable()
    {
        //Debug.Log("ayy");
        gameObject.SetActive(false);
    }
}
