using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoomListItem : MonoBehaviour
{
    public TMP_Text roomName;
    public TMP_Text numPlayers;
    public Button joinButton;

    RoomInfo info;
    public void Initialize(RoomInfo info)
    {
        this.info = info;
        roomName.text = info.Name;
        numPlayers.text = info.PlayerCount + " / " + info.MaxPlayers;
        joinButton.onClick.AddListener(JoinRoom);
    }

    public void JoinRoom()
    {
        NetworkManager.Instance.JoinRoom(info);
    }
}
