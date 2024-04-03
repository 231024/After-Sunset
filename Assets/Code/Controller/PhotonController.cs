using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

public sealed class PhotonController : MonoBehaviourPunCallbacks
{
    [Header("PhotonServerSettings")] 
    [SerializeField] private ServerSettings _serverSettings; 
    
    private TMP_Text _textProcess;
    
    protected const string LOADING_LOBBY_SCENE = "Lobby";
    protected string gameVersion = "1";
    
    private TypedLobby _sqlLobby = new TypedLobby("CustomSqlLobby", LobbyType.SqlLobby);
    
    [Inject] private MainGeneralViews _mainGeneralViews;

    
    public void Connect()
    {
        _textProcess = _mainGeneralViews.TextStatus;
        
        _textProcess.text = "";
        
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            ConnectionInfo("Connect", Color.blue);
            PhotonNetwork.JoinLobby();
        }else{
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }
    
    
    protected void ConnectionInfo(string message, Color color)
    {
        _textProcess.text = message;
        _textProcess.color = color;
    }

    void LogFeedback(string message)
    {
        if (_textProcess == null) {
            return;
        }
        _textProcess.text += System.Environment.NewLine+message;
        Debug.LogError(message);
    }

    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.IsConnected)
        {
            
            PhotonNetwork.JoinLobby(_sqlLobby);
        }
    }

    public override void OnJoinedLobby()
    {
        LogFeedback("Joining Room...");
        SceneManager.LoadScene(LOADING_LOBBY_SCENE);
    }
}