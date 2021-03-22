using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;

public class Launcher : MonoBehaviourPunCallbacks
{
    private static Launcher instance;
    public static Launcher Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<Launcher>();
            }
            return instance;
        }
    }
    public TMP_InputField roomNameInputField;
    public TMP_Text roomNameText;
    public TMP_Text errorText;
    public Transform roomListContent;
    public Transform playerListContent;
    public GameObject roomListItemPrefab;
    public GameObject playerListItemPrefab;
    public GameObject startGameButton;

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
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined lobby.");
        MenuManager.Instance.OpenMenu(MenuType.Main);
        PhotonNetwork.NickName = "Player" + Random.Range(0, 1000).ToString("0000");
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

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log(returnCode);
        MenuManager.Instance.OpenMenu(MenuType.Error);
        errorText.text = "Create room failed: " + message;
    }
    

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach(Transform t in roomListContent)
        {
            Destroy(t.gameObject);
        }
        for(int i = 0; i < roomList.Count; i++)
        {
            if (roomList[i].RemovedFromList)
            {
                continue;
            }
            GameObject item = Instantiate(roomListItemPrefab, roomListContent);
            item.GetComponent<RoomListItem>()?.Initialize(roomList[i]);
        }
    }
    public void JoinRoom(RoomInfo info)
    {
        PhotonNetwork.JoinRoom(info.Name);
        Debug.Log("joining room");
        MenuManager.Instance.OpenMenu(MenuType.Loading);
    }

    public override void OnJoinedRoom()
    {
        MenuManager.Instance.OpenMenu(MenuType.Room);
        roomNameText.text = PhotonNetwork.CurrentRoom.Name;

        Photon.Realtime.Player[] playerList = PhotonNetwork.PlayerList;
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        for (int i = 0; i < playerList.Length; i++)
        {
            GameObject item = Instantiate(playerListItemPrefab, playerListContent);
            item.GetComponent<PlayerListItem>()?.Initialize(playerList[i]);
        }
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }

    public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
    {
        base.OnMasterClientSwitched(newMasterClient);
        startGameButton.SetActive(PhotonNetwork.IsMasterClient);
    }
    public void StartGame()
    {
        PhotonNetwork.LoadLevel(1);
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

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        Debug.Log("entered room");
        GameObject item = Instantiate(playerListItemPrefab, playerListContent);
        item.GetComponent<PlayerListItem>()?.Initialize(newPlayer);
    }


}
