using System;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal sealed class ManagerLobbyWindowView : IStartable, IDisposable
{
    private Button _roomListGlobal;
    private Button _roomList;
    private Button _roomCreate;
    private Button _connectRoom;
    private Button _settingsMenu;
    
    
    RoomListPanelView _listRooomPanelView;
    HomeLobbyView _homeLobbyViewPanel;
    SettingsMenuView _settingsMenuView;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    
    public void Start()
    {
        _listRooomPanelView = _lobbyGeneralViews.RoomListPanel;
        _homeLobbyViewPanel = _lobbyGeneralViews.HomeLobbyViewPanel;
        _settingsMenuView = _lobbyGeneralViews.SettingsMenuView;
        
        _roomListGlobal = _lobbyGeneralViews.GlobalRoomButton;
        _settingsMenu = _lobbyGeneralViews.SettingsButton;
        
        _roomList = _listRooomPanelView.RoomListButton;
        _roomCreate = _listRooomPanelView.CreateRoomButton;
        _connectRoom = _listRooomPanelView.ConnectToRoom;
        
        _roomListGlobal.onClick.AddListener(OpenRoomListPanel);
        _settingsMenu.onClick.AddListener(OpenSettingMenuPanel);
        _connectRoom.onClick.AddListener(OpenRoomInfoPanel);
    }

    private void OpenRoomListPanel()
    {
        _homeLobbyViewPanel.enabled = false;
        _settingsMenuView.enabled = false;
        _listRooomPanelView.enabled = true;
    }

    private void OpenSettingMenuPanel()
    {
        _listRooomPanelView.enabled = false;
        _homeLobbyViewPanel.enabled = false;
        _settingsMenuView.enabled = true;
    }

    private void OpenRoomInfoPanel()
    {
        _listRooomPanelView.enabled = false;
        _settingsMenuView.enabled = false;
        _homeLobbyViewPanel.enabled = true;
    }

    public void Dispose()
    {
        _roomListGlobal.onClick.RemoveListener(OpenRoomListPanel);
        _settingsMenu.onClick.RemoveListener(OpenSettingMenuPanel);
        _connectRoom.onClick.RemoveListener(OpenRoomInfoPanel);
    }
}