using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    public TMP_InputField roomNameInputField;
    public TMP_InputField errorText;
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
        
    }

    public void CreateRoom()
    {
        PhotonNetwork.CreateRoom("Team1");
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Room);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode);
        MenuManager.Instance.OpenMenu(MenuType.Error);
        errorText.text = "Create room failed: " + message;
    }

    public void LeaveRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Loading);
        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.ServerList);
    }
    //public void JoinRoom(string roomName)
    //{
    //    PhotonNetwork.JoinRoom(roomName);
    //}
}
