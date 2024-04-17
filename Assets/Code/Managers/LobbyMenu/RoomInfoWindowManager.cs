using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class RoomInfoWindowManager: IStartable, IDisposable
{
    private HomeLobbyView _homeLobbyView;

    private Button _buttonCopyRoomName;
    private Transform _parentContent;
    private TMP_Text _labelRoomName;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private PhotonController _photonController;
    
    public void Start()
    {
        _homeLobbyView = _lobbyGeneralViews.HomeLobbyViewPanel;

        _labelRoomName = _homeLobbyView.InputFieldRoomName;
        _buttonCopyRoomName = _homeLobbyView.ButtonCopy;
        _parentContent = _homeLobbyView.ContentListPlayers;
        _labelRoomName.text = " ";

        _photonController.OnEnteredTheRoom += Init;
    }

    private void Init()
    {
        _labelRoomName.text = _photonController.GetCurrentRoom();
        var prefab = Resources.Load<GameObject>(UIConstants.INFO_PLAYER_NAME_ITEM_PREFAB);

        foreach (var player in _photonController.PlayerList)
        {
            var go = Object.Instantiate(prefab, _parentContent).
                GetComponent<InfoPlayerItemView>().LabelPlayerName.text = player.NickName;
        }
    }

    public void Dispose()
    {
        _photonController.OnEnteredTheRoom -= Init;
    }
}