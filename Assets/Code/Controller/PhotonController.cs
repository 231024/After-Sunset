using System;
using System.Collections.Generic;
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

    private const string LOADING_LOBBY_SCENE = "Lobby";
    private const string DEFAULT_ROOM_NAME = "Default";
    private const int MAX_PLAYERS = 4;

    private string gameVersion = "1";

    protected List<RoomInfo> _roomList = new List<RoomInfo>();
    
    private TypedLobby _sqlLobby;
    private RoomOptions _defaultRoomOptions;
    
    [Inject] private MainGeneralViews _mainGeneralViews;


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
        
        Debug.Log("Start");
        Connect();
    }
    
    public void Connect()
    {
        //PhotonNetwork.NickName = nickname;

        _textProcess = _mainGeneralViews.TextStatus;

        _textProcess.text = "";
        LogFeedback("Enter to Connect Method");

        if (!PhotonNetwork.IsConnected)
        {
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
            //PhotonNetwork.GameVersion = this.gameVersion;
        }
        else if (!PhotonNetwork.InLobby)
        {
            LogFeedback("Joining Room...");
            ConnectionInfo("Connect", Color.blue);
            PhotonNetwork.JoinLobby();
        }
    }

    public void JoinLobbyManual()
    {
        PhotonNetwork.JoinLobby(_sqlLobby);
    }
    
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }
    
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"[Photon] OnCreateRoomFailed with code: {returnCode} and message: {message}");
    }

    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.LogError($"[Photon] OnJoinRoomFailed with code: {returnCode} and message: {message}");
    }
    
    protected void ConnectionInfo(string message, Color color)
    {
        _textProcess.text = message;
        _textProcess.color = color;
    }

    protected void LogFeedback(string message)
    {
        _textProcess.text = "";
        if (_textProcess == null) {
            return;
        }
        _textProcess.text += System.Environment.NewLine+message;
        Debug.Log(message);
    }

    public override void OnConnectedToMaster()
    {
        LogFeedback($"IsConnected = {PhotonNetwork.IsConnected.ToString()}");
        
        //PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.JoinLobby(_sqlLobby);
        // if (PhotonNetwork.IsConnected)
        // {
        //     PhotonNetwork.JoinLobby();
        // }
    }

    public override void OnJoinedLobby()
    {
        LogFeedback($"IsConnected = {PhotonNetwork.IsConnected.ToString()}");
        LogFeedback($"InLobby = {PhotonNetwork.InLobby}");
        
        if (PhotonNetwork.IsConnected && PhotonNetwork.InLobby)
        {
            PhotonNetwork.CreateRoom(DEFAULT_ROOM_NAME);
            PhotonNetwork.CreateRoom("0", _defaultRoomOptions);
            SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        }

        //PhotonNetwork.JoinRandomRoom();
        if (PhotonNetwork.CountOfRooms == 0)
        {
            PhotonNetwork.CreateRoom(DEFAULT_ROOM_NAME);
            PhotonNetwork.CreateRoom("1", _defaultRoomOptions);
        }
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
        LogFeedback(PhotonNetwork.CountOfRooms.ToString());
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            Debug.Log($"OnRoomListUpdate {info.Name}, {info.PlayerCount}");
            PhotonNetwork.JoinRoom(info.Name);
            return;
        }

        PhotonNetwork.CreateRoom(Guid.NewGuid().ToString());
    }
    
    // public override void OnRoomListUpdate(List<RoomInfo> roomList)
    // {
    //     foreach (var info in roomList)
    //     {
    //         LogFeedback(info.Name);
    //     }
    //
    //     PhotonNetwork.CreateRoom(Guid.NewGuid().ToString());
    //     // foreach (var info in roomList)
    //     // {
    //     //     _roomList.Add(info);
    //     //     LogFeedback(info.Name);
    //     // }
    //     LogFeedback(_roomList.Count.ToString());
    // }
}