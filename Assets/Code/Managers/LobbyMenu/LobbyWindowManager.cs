using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal sealed class LobbyWindowManager : IStartable, IDisposable
{
    private Button _buttonRoomListGlobal;
    private Button _buttonCloseSettingMenu;
    private Button _buttonConnectRoom;
    private Button _buttonSettingsMenu;
    
    private string _roomName;
    private float _amountMaxPlayers;
    private bool _isWasListRoomWindow;

    private List<GameObject> _itemRoomInfos;
    
    private RoomListPanelView _listRooomPanelView;
    private HomeLobbyView _homeLobbyPanelView;
    private SettingsMenuView _settingsMenuView;
    private Transform _headerPanel;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private PhotonController _photonController;
    
    public void Start()
    {
        _itemRoomInfos = new List<GameObject>();
        _listRooomPanelView = _lobbyGeneralViews.RoomListPanel;
        _homeLobbyPanelView = _lobbyGeneralViews.HomeLobbyViewPanel;
        _settingsMenuView = _lobbyGeneralViews.SettingsMenuView;


        _buttonRoomListGlobal = _lobbyGeneralViews.GlobalRoomButton;
        _buttonSettingsMenu = _lobbyGeneralViews.SettingsButton;

        _buttonConnectRoom = _listRooomPanelView.ConnectToRoom;
        _buttonCloseSettingMenu = _settingsMenuView.CloseSettingMenu;

        _headerPanel = _lobbyGeneralViews.Header;

        _buttonRoomListGlobal.onClick.AddListener(OpenRoomListPanel);
        _buttonSettingsMenu.onClick.AddListener(OpenSettingMenuPanel);
        _buttonConnectRoom.onClick.AddListener(ConnectToRoom);
        _buttonCloseSettingMenu.onClick.AddListener(CloseSettingMenu);
        _photonController.OnEnteredTheRoom += OpenRoomInfoPanel;
        _listRooomPanelView.RoomNameInputField.onValueChanged.AddListener(ChangeRoomNameText);

        OpenRoomListPanel();
    }

    public void ChangeRoomNameText(string name)
    {
        _roomName = name;
        Debug.LogWarning($"ChangeRoomNameText - {_roomName}");
    }

    public void OpenRoomListPanel()
    {
        _homeLobbyPanelView.gameObject.SetActive(false);
        _settingsMenuView.gameObject.SetActive(false);
        _listRooomPanelView.gameObject.SetActive(true);
        _headerPanel.gameObject.SetActive(true);
        _isWasListRoomWindow = true;
        _buttonRoomListGlobal.interactable = false;
        CreateItemInfoRooms();
    }

    private void OpenSettingMenuPanel()
    {
        _listRooomPanelView.gameObject.SetActive(false);
        _homeLobbyPanelView.gameObject.SetActive(false);
        _headerPanel.gameObject.SetActive(false);
        _settingsMenuView.gameObject.SetActive(true);
    }

    public void OpenRoomInfoPanel()
    {
        _listRooomPanelView.gameObject.SetActive(false);
        _settingsMenuView.gameObject.SetActive(false);
        _homeLobbyPanelView.gameObject.SetActive(true);
        _headerPanel.gameObject.SetActive(true);
        _isWasListRoomWindow = false;
    }

    private void CloseSettingMenu()
    {
        if (_isWasListRoomWindow)
        {
            OpenRoomListPanel();
        }
        else
        {
            OpenRoomInfoPanel();
        }
    }

    private void ConnectToRoom()
    {
        Debug.LogWarning($"ConnectToRoom - {_roomName}");
        _photonController.SetNameForJoiningRoom(_roomName);
        //OpenRoomInfoPanel();
    }

    private void CreateItemInfoRooms()
    {
        foreach (var itemRoomInfo in _itemRoomInfos)
        {
            GameObject.Destroy(itemRoomInfo);
        }
        _itemRoomInfos?.Clear();
        
        var roomListPanel = _lobbyGeneralViews.RoomListPanel;
        Debug.Log($"CreateItemInfoRooms - {_photonController.CachedRoomList.Count}");
        var prefab = Resources.Load<GameObject>(UIConstants.INFO_ROOM_ITEM_PREFAB);

        if (_photonController.CachedRoomList != null)
            foreach (var roomInfo in _photonController.CachedRoomList)
            {
                if (roomInfo.Value.IsVisible)
                {
                    var itemRoomInfo = GameObject.Instantiate(prefab, 
                        roomListPanel.ParentTransformContent);
                    var view = itemRoomInfo.GetComponent<InfoRoomItemView>();
                    view.LabelRoomName.text = roomInfo.Value.Name;
                    view.LabelAmountPlayers.text = roomInfo.Value.PlayerCount.ToString();
                    view.LabelMapName.text = UIConstants.DEFAULT_MAP_NAME;
                    view.InfoRoomItemButton.onClick.AddListener((() =>
                    {
                        _photonController.SetNameForJoiningRoom(view.LabelRoomName.text);
                    }));
                    _itemRoomInfos?.Add(itemRoomInfo);
                }
            }
    }

    public void Dispose()
    {
        _buttonRoomListGlobal.onClick.RemoveListener(OpenRoomListPanel);
        _buttonSettingsMenu.onClick.RemoveListener(OpenSettingMenuPanel);
        _buttonConnectRoom.onClick.RemoveListener(OpenRoomInfoPanel);
        _buttonCloseSettingMenu.onClick.RemoveListener(CloseSettingMenu);
        _photonController.OnEnteredTheRoom -= OpenRoomInfoPanel;
        _listRooomPanelView.RoomNameInputField.onValueChanged.RemoveListener(ChangeRoomNameText);
    }
}