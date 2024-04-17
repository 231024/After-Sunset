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

    public Action<string> OnPublishedStatusProcess;
    public Action OnEnteredTheRoom;

    private const int MAX_PLAYERS = 4;
    private const string LOADING_LOBBY_SCENE = "Lobby";
    private const string GAME_VERSION = "1";
    
    private string _nickname;

    public List<RoomInfo> RoomList => _roomList;

    [Inject] private readonly IPublisher<string, string> _publisher;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        Debug.Log("Start");
        _roomList = new List<RoomInfo>();
        Connect();
    }

    public void Connect()
    {
        LogFeedback("Enter to Connect Method");

        if (!PhotonNetwork.IsConnected)
        {
            LogFeedback("Connecting...");
            PhotonNetwork.GameVersion = GAME_VERSION;
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
        }
        else if (!PhotonNetwork.InLobby)
        {
            LogFeedback("[Connect] Joining Lobby...");
            PhotonNetwork.JoinLobby();
        }
    }
    
    public void NicknameReceived(string nickname)
    {
        _nickname = nickname;
        LogFeedback($"[NicknameReceived] nickname = {_nickname}");
        PhotonNetwork.NickName = _nickname;
    }
    
    protected void ConnectionInfo(string message, Color color)
    {
        _textProcess.text = message;
        _textProcess.color = color;
    }

    public void CreateRoom(string roomName, float maxPlayers, bool privacy)
    {
        var option = new RoomOptions
        {
            IsVisible = privacy,
            MaxPlayers = Convert.ToInt32(maxPlayers)
        };
        PhotonNetwork.CreateRoom(roomName, option);
    }

    public string GetCurrentRoom()
    {
        return PhotonNetwork.CurrentRoom.Name;
    }

    protected void LogFeedback(string message)
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
        LogFeedback($"[OnJoinedLobby] IsConnected = {PhotonNetwork.IsConnected.ToString()}");
        LogFeedback($"[OnJoinedLobby] InLobby = {PhotonNetwork.InLobby}");
        
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        }
    }

    public override void OnCreatedRoom()
    {
        OnEnteredTheRoom?.Invoke();
    }

    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
        LogFeedback("OnRoomListUpdate");
        foreach (var info in roomList)
        {
            LogFeedback(info.Name);
        }
        Debug.Log($"OnRoomListUpdate {_roomList.Count}");
        LogFeedback(_roomList.Count.ToString());
    }

    public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
    {
        LogFeedback($"[OnPlayerEnteredRoom] Player = {newPlayer.NickName}");
    }
}
