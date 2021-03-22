using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using System.IO;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    public GameObject localPlayer;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    private void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {
        Debug.Log("instantiated playercontroller");
        GameObject o = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);
        PhotonNetwork.LocalPlayer.TagObject = o;
        Debug.Log(PhotonNetwork.LocalPlayer.TagObject);
        PhotonNetwork.GetPhotonView(PV.ViewID).Owner.TagObject = o;
    }


}
