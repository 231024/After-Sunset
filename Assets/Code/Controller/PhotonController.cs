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

    private string gameVersion = "1";

    protected List<RoomInfo> _roomList = new List<RoomInfo>();

    protected TypedLobby _sqlLobby;
    private RoomOptions _defaultRoomOptions;
    
    //[Inject] private MainGeneralViews _mainGeneralViews;
    [Inject] private readonly IPublisher<string, string> _publisher;


    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void Start()
    {
        _sqlLobby = new TypedLobby("CustomSqlLobby", LobbyType.SqlLobby);
        _defaultRoomOptions = new RoomOptions
        {
            IsVisible = true,
            IsOpen = true,
            MaxPlayers = MAX_PLAYERS
        };
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
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
            PhotonNetwork.GameVersion = this.gameVersion;
        }
        else if (!PhotonNetwork.InLobby)
        {
            LogFeedback("Joining Room...");
            //ConnectionInfo("Connect", Color.blue);
            PhotonNetwork.JoinLobby();
        }
    }
    
    public void NicknameRecieved(string nickname)
    {
        PhotonNetwork.NickName = nickname;
    }

    public void JoinLobbyManual()
    {
        PhotonNetwork.JoinLobby(_sqlLobby);
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
        LogFeedback($"IsConnected = {PhotonNetwork.IsConnected.ToString()}");
    }

    public override void OnJoinedLobby()
    {
        LogFeedback($"IsConnected = {PhotonNetwork.IsConnected.ToString()}");
        LogFeedback($"InLobby = {PhotonNetwork.InLobby}");
        
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        }
        
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        LogFeedback(PhotonNetwork.CountOfRooms.ToString());
    }
    
    
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
        foreach (var info in roomList)
        {
            LogFeedback(info.Name);
        }
        Debug.Log($"OnRoomListUpdate {_roomList.Count.ToString()}");
        LogFeedback(_roomList.Count.ToString());
    }
}