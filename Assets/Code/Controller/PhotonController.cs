using System;
using System.Collections.Generic;
using MessagePipe;
using Photon.Pun;
using Photon.Realtime;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public class PhotonController : MonoBehaviourPunCallbacks
{
    [Header("PhotonServerSettings")] 
    [SerializeField] private ServerSettings _serverSettings; 
    
    private TMP_Text _textProcess;
    private string _stringStatusProcces;

    private const string LOADING_LOBBY_SCENE = "Lobby";
    private const string DEFAULT_ROOM_NAME = "Default";
    private const int MAX_PLAYERS = 4;
    private const string GAME_VERSION = "1";
    
    protected List<RoomInfo> _roomList = new List<RoomInfo>();
    [Inject] private readonly IPublisher<string, string> _publisher;

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _publisher.Publish(UIConstants.TEXT_STATUS, _stringStatusProcces);
        Debug.Log("Start");
        Connect();
    }
    
    public void Connect()
    {
        _stringStatusProcces = "";
        LogFeedback("Enter to Connect Method");

        if (!PhotonNetwork.IsConnected)
        {
            LogFeedback("[Connect] Connecting...");
            PhotonNetwork.GameVersion = GAME_VERSION;
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
        }
        else if (!PhotonNetwork.InLobby)
        {
            LogFeedback("[Connect] Joining Lobby...");
            PhotonNetwork.JoinLobby();
        }
    }
    
    public void NicknameRecieved(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }
    
    protected void ConnectionInfo(string message, Color color)
    {
        _textProcess.text = message;
        _textProcess.color = color;
    }

    protected void LogFeedback(string message)
    {
        _stringStatusProcces = "";
        _stringStatusProcces = System.Environment.NewLine+message;
        Debug.Log(message);
    }

    public override void OnConnectedToMaster()
    {
        LogFeedback($"[OnConnectedToMaster] IsConnected = {PhotonNetwork.IsConnected.ToString()}");
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
        LogFeedback($"[OnCreatedRoom] PlayerCount {PhotonNetwork.CurrentRoom.PlayerCount}");
        LogFeedback($"[OnCreatedRoom] CurrentRoom Name {PhotonNetwork.CurrentRoom.Name}");
        LogFeedback($"[OnCreatedRoom] LocalPlayer Name {PhotonNetwork.NickName}");
        LogFeedback($"[OnCreatedRoom] In Lobby {PhotonNetwork.InLobby}");
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        _roomList = roomList;
        Debug.Log("OnRoomListUpdate");
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
