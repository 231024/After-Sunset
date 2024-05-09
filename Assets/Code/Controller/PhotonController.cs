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
    private Dictionary<string, RoomInfo> _cachedRoomList;
    private Photon.Realtime.Player[] _playerList;

    public Action<string> OnPublishedStatusProcess;
    public Action OnEnteredTheRoom;

    private const string LOADING_LOBBY_SCENE = "Lobby";
    private const string GAME_VERSION = "1";
    
    private string _nickname;

    public string Nickname => _nickname;
    public Dictionary<string, RoomInfo> CachedRoomList => _cachedRoomList;
    public Photon.Realtime.Player[] PlayerList => _playerList;

    [Inject] private readonly IPublisher<string, string> _publisher;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _cachedRoomList = new Dictionary<string, RoomInfo>();
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
            IsVisible = privacy,
            MaxPlayers = Convert.ToInt32(maxPlayers)
        };
        PhotonNetwork.JoinOrCreateRoom(roomName, option, null);
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
        Debug.LogWarning($"[PhotonController] SetNameForJoiningRoom - {roomName}");
        PhotonNetwork.JoinRoom(roomName);
    }

    public void EnterTheGame(string gameScene)
    {
        PhotonNetwork.LoadLevel(gameScene);
    }

    private void LogFeedback(string message)
    {
        OnPublishedStatusProcess?.Invoke(message);
    }

    public override void OnConnectedToMaster()
    {
        LogFeedback($"Connected To Master");
    }

    public override void OnJoinedLobby()
    {
        _cachedRoomList.Clear();
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        }
    }
    
    public override void OnLeftLobby()
    {
        _cachedRoomList.Clear();
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

    private void UpdateCachedRoomList(List<RoomInfo> roomList)
    {
        for(int i=0; i<roomList.Count; i++)
        {
            RoomInfo info = roomList[i];
            if (info.RemovedFromList)
            {
                _cachedRoomList.Remove(info.Name);
            }
            else
            {
                _cachedRoomList[info.Name] = info;
            }
        }
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        UpdateCachedRoomList(roomList);
    }
    
    public override void OnDisconnected(DisconnectCause cause)
    {
        _cachedRoomList.Clear();
    }
}
