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


    private void Start()
    {
        _sqlLobby = new TypedLobby("CustomSqlLobby", LobbyType.SqlLobby);
        _defaultRoomOptions = new RoomOptions();
        _defaultRoomOptions.MaxPlayers = MAX_PLAYERS;
        _defaultRoomOptions.IsOpen = true;
        _defaultRoomOptions.IsVisible = true;
    }


    public void Connect()
    {
        LogFeedback("Enter to Connect Method");
        
        _textProcess = _mainGeneralViews.TextStatus;
        
        _textProcess.text = "";
        
        if (!PhotonNetwork.IsConnected)
        {
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
            PhotonNetwork.GameVersion = this.gameVersion;
        }
        else if (PhotonNetwork.IsConnected && !PhotonNetwork.InLobby)
        {
            LogFeedback("Joining Room...");
            ConnectionInfo("Connect", Color.blue);
            PhotonNetwork.JoinLobby();
        }
    }
    
    
    protected void ConnectionInfo(string message, Color color)
    {
        _textProcess.text = message;
        _textProcess.color = color;
    }

    protected void LogFeedback(string message)
    {
        if (_textProcess == null) {
            return;
        }
        _textProcess.text += System.Environment.NewLine+message;
        Debug.Log(message);
    }

    public override void OnConnectedToMaster()
    {
        //PhotonNetwork.JoinRandomRoom();
        //PhotonNetwork.JoinLobby(_sqlLobby);
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby(_sqlLobby);
        }
    }

    public override void OnJoinedLobby()
    {
        LogFeedback(PhotonNetwork.IsConnected.ToString());
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.CreateRoom(DEFAULT_ROOM_NAME, _defaultRoomOptions, _sqlLobby);
            SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        }
        //PhotonNetwork.JoinRandomRoom();
        LogFeedback(PhotonNetwork.CurrentLobby.Name);
        // if (PhotonNetwork.CountOfRooms == 0)
        // {
        //     _defaultRoomOptions.MaxPlayers = MAX_PLAYERS;
        //     _defaultRoomOptions.IsOpen = true;
        //     _defaultRoomOptions.IsVisible = true;
        //     PhotonNetwork.CreateRoom("1");
        // }
    }

    public override void OnCreatedRoom()
    {
        LogFeedback("Created");
        LogFeedback(PhotonNetwork.CountOfRooms.ToString());
    }
    
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        foreach (var info in roomList)
        {
            _roomList.Add(info);
            LogFeedback(info.Name);
        }
        LogFeedback(_roomList.Count.ToString());
    }
}