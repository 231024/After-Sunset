using System;
using TMPro;
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
    
    private RoomListPanelView _listRooomPanelView;
    private HomeLobbyView _homeLobbyPanelView;
    private SettingsMenuView _settingsMenuView;
    private CreateRoomPanelView _createRoomPanelView;
    private Transform _headerPanel;
    
    private TMP_InputField _roomNameTMPInputField;
    private Slider _maxPlayersSlider;
    private TMP_Text _maxPlayersTMPText;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private PhotonController _photonController;
    
    public void Start()
    {
        _listRooomPanelView = _lobbyGeneralViews.RoomListPanel;
        _homeLobbyPanelView = _lobbyGeneralViews.HomeLobbyViewPanel;
        _settingsMenuView = _lobbyGeneralViews.SettingsMenuView;
        _createRoomPanelView = _lobbyGeneralViews.RoomListPanel.CreateRoomPanelView;


        _buttonRoomListGlobal = _lobbyGeneralViews.GlobalRoomButton;
        _buttonSettingsMenu = _lobbyGeneralViews.SettingsButton;

        _buttonConnectRoom = _listRooomPanelView.ConnectToRoom;
        _buttonCloseSettingMenu = _settingsMenuView.CloseSettingMenu;

        _headerPanel = _lobbyGeneralViews.Header;
        _roomNameTMPInputField = _createRoomPanelView.RoomNameCreate;
        _maxPlayersSlider = _createRoomPanelView.AmountPlayerSlider;
        _maxPlayersTMPText = _createRoomPanelView.TextMaxPlayers;

        _buttonRoomListGlobal.onClick.AddListener(OpenRoomListPanel);
        _buttonSettingsMenu.onClick.AddListener(OpenSettingMenuPanel);
        _buttonConnectRoom.onClick.AddListener(OpenRoomInfoPanel);
        _buttonCloseSettingMenu.onClick.AddListener(CloseSettingMenu);
        _roomNameTMPInputField.onValueChanged.AddListener(OnChangedRoomName);
        _maxPlayersSlider.onValueChanged.AddListener(OnChangedAmountMaxPayers);
        _createRoomPanelView.ButtonCreateRoom.onClick.AddListener(CreateRoom);

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
    
    private void CreateRoom()
    {
        _photonController.CreateRoom(_roomName, _amountMaxPlayers, _createRoomPanelView.TogglePrivacy);
        OpenRoomInfoPanel();
    }
    
    private void OnChangedAmountMaxPayers(float amount)
    {
        _amountMaxPlayers = amount;
        _maxPlayersTMPText.text = amount.ToString();
    }

    private void OnChangedRoomName(string roomName)
    {
        _roomName = roomName;
    }

    private void CreateItemInfoRooms()
    {
        Debug.Log("CreateItemInfoRooms");
        var roomListPanel = _lobbyGeneralViews.RoomListPanel;

        if (_photonController?.RoomList != null)
            foreach (var roomInfo in _photonController.RoomList)
            {
                Debug.Log("[CreateItemInfoRooms] CreateItem");
                var itemRoomInfo = GameObject.Instantiate(Resources.Load<GameObject>(UIConstants.INFO_ROOM_ITEM_PREFAB),
                    roomListPanel.ParentTransformContent);
                var view = itemRoomInfo.GetComponent<InfoRoomItemView>();
                view.LabelRoomName.text = roomInfo.Name;
                view.LabelAmountPlayers.text = roomInfo.PlayerCount.ToString();
                view.LabelMapName.text = UIConstants.DEFAULT_MAP_NAME;
            }
    }

    public void Dispose()
    {
        _buttonRoomListGlobal.onClick.RemoveListener(OpenRoomListPanel);
        _buttonSettingsMenu.onClick.RemoveListener(OpenSettingMenuPanel);
        _buttonConnectRoom.onClick.RemoveListener(OpenRoomInfoPanel);
        _buttonCloseSettingMenu.onClick.RemoveListener(CloseSettingMenu);
        _roomNameTMPInputField.onValueChanged.RemoveListener(OnChangedRoomName);
        _maxPlayersSlider.onValueChanged.RemoveListener(OnChangedAmountMaxPayers);
        _createRoomPanelView.ButtonCreateRoom.onClick.RemoveListener(CreateRoom);
    }
}