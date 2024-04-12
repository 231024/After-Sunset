using System;
using TMPro;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

public class RoomInfoWindowManager: IStartable, IDisposable
{
    private HomeLobbyView _homeLobbyView;

    private Button _buttonCopyRoomName;
    private TMP_Text _labelRoomName;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private PhotonController _photonController;
    
    public void Start()
    {
        _homeLobbyView = _lobbyGeneralViews.HomeLobbyViewPanel;

        _labelRoomName = _homeLobbyView.InputFieldRoomName;
        _buttonCopyRoomName = _homeLobbyView.ButtonCopy;

        Init();
    }

    private void Init()
    {
        _labelRoomName.text = _photonController.GetCurrentRoom();
    }

    public void Dispose()
    {
        
    }
}