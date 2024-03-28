using Photon.Pun;
using Photon.Realtime;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PhotonController : MonoBehaviourPunCallbacks
{
    [Header("PhotonServerSettings")] 
    [SerializeField] private ServerSettings _serverSettings; 
    [SerializeField] private TMP_Text _textProcess;
    [SerializeField] private Image _bgConnection;
    
    protected const string LOADING_LOBBY_SCENE = "Lobby";
    protected string gameVersion = "1";
    
    private TypedLobby _sqlLobby = new TypedLobby("CustomSqlLobby", LobbyType.SqlLobby);

    
    public void Connect()
    {
        _textProcess.text = "";
        
        if (PhotonNetwork.IsConnected)
        {
            LogFeedback("Joining Room...");
            PhotonNetwork.JoinLobby();
        }else{
            LogFeedback("Connecting...");
            PhotonNetwork.ConnectUsingSettings(_serverSettings.AppSettings);
            PhotonNetwork.GameVersion = this.gameVersion;
        }
    }
    
    
    protected void ConnectionInfo(bool enable, string message, Color color)
    {
        _bgConnection.enabled = enable;
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
            Debug.LogWarning("OnConnectedToMaster: Next -> try to Join Lobby");
        }
    }

    public override void OnJoinedLobby()
    {
        LogFeedback("OnJoinedLobby: Next -> try to LoadScene(1)");
        Debug.LogWarning("OnJoinedLobby");
        SceneManager.LoadScene(LOADING_LOBBY_SCENE);
        //SceneManager.LoadScene(1);
    }
}