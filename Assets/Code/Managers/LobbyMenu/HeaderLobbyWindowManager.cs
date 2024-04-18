using VContainer;
using VContainer.Unity;

public class HeaderLobbyWindowManager : IStartable
{
    private HeaderLobbyView _headerLobbyView;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private PhotonController _photonController;
    
    public void Start()
    {
        _headerLobbyView = _lobbyGeneralViews.HeaderLobbyView;
        SetUsername(_photonController.Nickname);
    }

    public void SetUsername(string username)
    {
        _headerLobbyView.TextUsername.text = username;
    }
}
