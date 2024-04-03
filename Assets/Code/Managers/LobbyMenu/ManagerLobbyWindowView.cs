using System;
using UnityEngine;
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
    
    [Inject] private LobbyGeneralViews LobbyGeneralViews;
    
    
    public void Start()
    {
        _listRooomPanelView = LobbyGeneralViews.RoomListPanel;
        _homeLobbyViewPanel = LobbyGeneralViews.HomeLobbyViewPanel;
        _settingsMenuView = LobbyGeneralViews.SettingsMenuView;
        
        _roomListGlobal = LobbyGeneralViews.GlobalRoomButton;
        _settingsMenu = LobbyGeneralViews.SettingsButton;
        
        _roomList = _listRooomPanelView.RoomListButton;
        _roomCreate = _listRooomPanelView.CreateRoomButton;
        _connectRoom = _listRooomPanelView.ConnectToRoom;
        
        _roomListGlobal.onClick.AddListener(OpenRoomListPanel);
        _settingsMenu.onClick.AddListener(OpenSettingMenuPanel);
        _connectRoom.onClick.AddListener(OpenRoomInfoPanel);

        OpenRoomListPanel();
    }

    private void OpenRoomListPanel()
    {
        _homeLobbyViewPanel.GetComponent<Canvas>().enabled = false;
        _settingsMenuView.GetComponent<Canvas>().enabled = false;
        _listRooomPanelView.GetComponent<Canvas>().enabled = true;
    }

    private void OpenSettingMenuPanel()
    {
        _listRooomPanelView.GetComponent<Canvas>().enabled = false;
        _homeLobbyViewPanel.GetComponent<Canvas>().enabled = false;
        _settingsMenuView.GetComponent<Canvas>().enabled = true;
    }

    private void OpenRoomInfoPanel()
    {
        _listRooomPanelView.GetComponent<Canvas>().enabled = false;
        _settingsMenuView.GetComponent<Canvas>().enabled = false;
        _homeLobbyViewPanel.GetComponent<Canvas>().enabled = true;
    }

    public void Dispose()
    {
        _roomListGlobal.onClick.RemoveListener(OpenRoomListPanel);
        _settingsMenu.onClick.RemoveListener(OpenSettingMenuPanel);
        _connectRoom.onClick.RemoveListener(OpenRoomInfoPanel);
    }
}