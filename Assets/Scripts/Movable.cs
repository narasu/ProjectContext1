using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Movable : Interactable
{
    [HideInInspector] public float resizeScalerX;
    [HideInInspector] public float resizeScalerY;
    [HideInInspector] public float resizeScalerZ;
    public Color Color = Color.red;
    public bool SetColorOnStart = false;
    protected HSVPicker.ColorPicker picker;
    bool isGrabbed;
    Transform playerTransform;
    Transform hand;
    Quaternion startRotation;
    PlayerTransformInHand transformInHand;
    PlayerEditMaterial playerEditMaterial;


    protected override void Awake()
    {
        base.Awake();
        //playerTransform = FindObjectOfType<Player>().transform;
        //transformInHand = FindObjectOfType<PlayerTransformInHand>();
        //hand = GameObject.FindGameObjectWithTag("Hand").transform;
    }

    //called from InteractingState.Update()
    public override void HandleInteraction()
    {
        base.HandleInteraction();
    }

    public virtual void Grab(Transform player)
    {
        Debug.Log("PV" + PV.IsMine);
        Debug.Log("PlayerPV" + player.GetComponent<Player>().PV.IsMine);
        PV.TransferOwnership(player.GetComponent<Player>().networkPlayer);
        //if (!PV.IsMine && player.GetComponent<Player>().PV.IsMine)
        //{
        //    PV.TransferOwnership(PhotonNetwork.LocalPlayer);
        //}
        //if (player.GetComponent<Player>().PV.IsMine)
        //{
        //    PV.TransferOwnership(PhotonNetwork.LocalPlayer);
        //}
        playerTransform = player;
        transform.SetParent(player.GetChild(1).transform);
        isGrabbed = true;
        ResetHandPosition();

        startRotation = transform.localRotation;
    }

    public void ResetHandPosition()
    {
        if (hand==null)
        {
            return;
        }
        Vector3 heading = transform.position - hand.position;
        hand.position += Vector3.Project(heading, Camera.main.transform.forward);
    }

    protected override void Update()
    {
        base.Update();
        if(isGrabbed)
        {
            transform.position = new Vector3(transform.position.x, playerTransform.GetComponent<Player>().Hand.transform.position.y, transform.position.z);
        }
    }

    public virtual void Drop()
    {
        
        fsm.GotoState(InteractableStateType.Normal);
        transform.SetParent(null);
        playerTransform = null;
        isGrabbed = false;
    }
    public virtual void ActiveEditMaterial() {}
    public virtual void DeactiveEditMaterial() {}
}
