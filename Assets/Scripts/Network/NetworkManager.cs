using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    [SerializeField] TMP_InputField roomNameInputField;
    void Start()
    {
        Debug.Log("Connecting to master...");
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master.");
        PhotonNetwork.JoinLobby();
        
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");
        MenuManager.Instance.OpenMenu(MenuType.ServerList);
        CreateRooms();
    }

    public void CreateRooms()
    {

        PhotonNetwork.CreateRoom("Team1");
        //PhotonNetwork.CreateRoom("Team2");
    }

    //public void JoinRoom(string roomName)
    //{
    //    PhotonNetwork.JoinRoom(roomName);
    //}
}
