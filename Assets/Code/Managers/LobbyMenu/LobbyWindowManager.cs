using System;
using MessagePipe;
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

    private bool _isWasListRoomWindow;
    
    private RoomListPanelView _listRooomPanelView;
    private HomeLobbyView _homeLobbyPanelView;
    private SettingsMenuView _settingsMenuView;
    private Transform _headerPanel;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private readonly IPublisher<LobbyWindowManager> _publisher;
    
    public void Start()
    {
        _publisher.Publish(this);
        
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
        _buttonConnectRoom.onClick.AddListener(OpenRoomInfoPanel);
        _buttonCloseSettingMenu.onClick.AddListener(CloseSettingMenu);

        OpenRoomListPanel();
    }

    private void OpenRoomListPanel()
    {
        _homeLobbyPanelView.gameObject.SetActive(false);
        _settingsMenuView.gameObject.SetActive(false);
        _listRooomPanelView.gameObject.SetActive(true);
        _headerPanel.gameObject.SetActive(true);
        _isWasListRoomWindow = true;
        _buttonRoomListGlobal.interactable = false;
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

    public void Dispose()
    {
        _buttonRoomListGlobal.onClick.RemoveListener(OpenRoomListPanel);
        _buttonSettingsMenu.onClick.RemoveListener(OpenSettingMenuPanel);
        _buttonConnectRoom.onClick.RemoveListener(OpenRoomInfoPanel);
        _buttonCloseSettingMenu.onClick.RemoveListener(CloseSettingMenu);
    }
}