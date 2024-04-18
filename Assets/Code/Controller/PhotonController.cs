using System;
using System.Collections.Generic;
using MessagePipe;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

public class PhotonController : MonoBehaviourPunCallbacks
{
    [Header("PhotonServerSettings")] 
    [SerializeField] private ServerSettings _serverSettings; 
    
    private TMP_Text _textProcess;
    private List<RoomInfo> _roomList;
    private Photon.Realtime.Player[] _playerList;

    public Action<string> OnPublishedStatusProcess;
    public Action OnEnteredTheRoom;

    private const string LOADING_LOBBY_SCENE = "Lobby";
    private const string GAME_VERSION = "1";
    
    private string _nickname;

    public string Nickname => _nickname;
    public List<RoomInfo> RoomList => _roomList;
    public Photon.Realtime.Player[] PlayerList => _playerList;

    [Inject] private readonly IPublisher<string, string> _publisher;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _roomList = new List<RoomInfo>();
        _playerList = Array.Empty<Photon.Realtime.Player>();
        Connect();
    }

    public void Connect()
    {
        if (!PhotonNetwork.IsConnected)
        {
            LogFeedback("Connecting...");
            PhotonNetwork.GameVersion = GAME_VERSION;
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
        }
        else if (!PhotonNetwork.InLobby)
        {
            LogFeedback("Joining Lobby...");
            PhotonNetwork.JoinLobby();
        }
    }
    
    public void NicknameReceived(string nickname)
    {
        _nickname = nickname;
        LogFeedback($"Nickname = {_nickname}");
        PhotonNetwork.NickName = _nickname;
    }

    public void CreateRoom(string roomName, float maxPlayers, bool privacy)
    {
        var option = new RoomOptions
        {
            IsOpen = privacy,
            MaxPlayers = Convert.ToInt32(maxPlayers)
        };
        PhotonNetwork.CreateRoom(roomName, option);
    }

    public string GetCurrentRoom()
    {
        Debug.Log(PhotonNetwork.CurrentRoom.Name);
        return PhotonNetwork.CurrentRoom.Name;
    }

    public void SetOpenedRoom(bool isOpened)
    {
        PhotonNetwork.CurrentRoom.IsOpen = isOpened;
    }

    public void LeaveTheRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void SetNameForJoiningRoom(string roomName)
    {
        PhotonNetwork.JoinRoom(roomName);
    }

    public void EnterTheGame(string gameScene)
    {
        PhotonNetwork.LoadLevel(gameScene);
    }

    private void LogFeedback(string message)
    {
        OnPublishedStatusProcess?.Invoke(message);
        Debug.Log(message);
    }

    public override void OnConnectedToMaster()
    {
        LogFeedback($"Connected To Master");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log($"OnJoinedLobby");
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        }
    }

    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        Connect();
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        _playerList = PhotonNetwork.PlayerList;
        OnEnteredTheRoom?.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
        foreach (var info in roomList)
        {
            LogFeedback(info.Name);
        }
        Debug.Log($"OnRoomListUpdate {_roomList.Count}");
    }
}
