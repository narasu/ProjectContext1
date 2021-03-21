using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class NetworkManager : MonoBehaviourPunCallbacks
{
    private static NetworkManager instance;
    public static NetworkManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<NetworkManager>();
            }
            return instance;
        }
    }
    public TMP_InputField roomNameInputField;
    public TMP_Text roomNameText;
    public TMP_Text errorText;
    public Transform roomListContent;
    public GameObject roomListItemPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        if (instance != this)
        {
            Destroy(gameObject);
        }
    }
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
        MenuManager.Instance.OpenMenu(MenuType.Main);
        
    }

    public void CreateRoom()
    {
        if (string.IsNullOrEmpty(roomNameInputField.text))
        {
            return;
        }
        MenuManager.Instance.OpenMenu(MenuType.Loading);
        PhotonNetwork.CreateRoom(roomNameInputField.text);
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Room);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;
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
        MenuManager.Instance.OpenMenu(MenuType.Main);
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform t in roomListContent)
        {
            Destroy(t.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            GameObject item = Instantiate(roomListItemPrefab, roomListContent);
            item.GetComponent<RoomListItem>()?.Initialize(roomList[i]);
        }
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }
}
