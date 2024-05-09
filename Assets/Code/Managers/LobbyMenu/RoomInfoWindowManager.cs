using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

public class RoomInfoWindowManager: IStartable, IDisposable
{
    private HomeLobbyView _homeLobbyView;
    
    private Transform _parentContent;
    private TMP_Text _labelRoomName;
    
    private List<GameObject> _itemPlayerInfos;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private PhotonController _photonController;
    [Inject] private LobbyWindowManager _lobbyWindowManager;
    
    public void Start()
    {
        _itemPlayerInfos = new List<GameObject>();
        
        _homeLobbyView = _lobbyGeneralViews.HomeLobbyViewPanel;

        _labelRoomName = _homeLobbyView.InputFieldRoomName;
        _parentContent = _homeLobbyView.ContentListPlayers;
        _labelRoomName.text = " ";

        _photonController.OnEnteredTheRoom += Init;
        _homeLobbyView.ButtonCloseRoom.onClick.AddListener(SetClosedRoom);
        _homeLobbyView.ButtonLeaveRoom.onClick.AddListener(LeaveRoom);
        _homeLobbyView.ButtonStart.onClick.AddListener((() =>
        {
            _photonController.EnterTheGame(GameConstants.DEFAULT_SCENE);
        }));
    }

    private void Init()
    {
        foreach (var itemPlayerInfo in _itemPlayerInfos)
        {
            GameObject.Destroy(itemPlayerInfo);
        }
        _itemPlayerInfos?.Clear();
        _labelRoomName.text = _photonController.GetCurrentRoom();
        var prefab = Resources.Load<GameObject>(UIConstants.INFO_PLAYER_NAME_ITEM_PREFAB);

        foreach (var player in _photonController.PlayerList)
        {
            var go = GameObject.Instantiate(prefab, _parentContent);
            go.GetComponent<InfoPlayerItemView>().LabelPlayerName.text = player.NickName;
            _itemPlayerInfos.Add(go);
        }
    }

    private void SetClosedRoom()
    {
        _photonController.SetOpenedRoom(false);
    }

    private void LeaveRoom()
    {
        _photonController.LeaveTheRoom();
        _lobbyWindowManager.OpenRoomListPanel();
    }

    public void Dispose()
    {
        _photonController.OnEnteredTheRoom -= Init;
        _homeLobbyView.ButtonCloseRoom.onClick.RemoveListener(SetClosedRoom);
        _homeLobbyView.ButtonLeaveRoom.onClick.RemoveListener(LeaveRoom);
        _homeLobbyView.ButtonStart.onClick.RemoveListener((() =>
        {
            _photonController.EnterTheGame(GameConstants.DEFAULT_SCENE);
        }));
    }
}