using TMPro;
using UnityEngine;
using UnityEngine.UI;


internal sealed class LobbyGeneralViews : MonoBehaviour
{
    [Header("Global UI Elements")]
    [SerializeField] private Button _roomInfoButton;
    [SerializeField] private Button _globalRoomButton;
    [SerializeField] private Button _settingsButton;

    [Header("Children Views")] 
    [SerializeField] private RoomListPanelView _roomListPanel;
    [SerializeField] private HomeLobbyView _homeLobbyViewPanel;
    [SerializeField] private SettingsMenuView _settingsMenuView;

    public Button RoomInfoButton => _roomInfoButton;

    public Button GlobalRoomButton => _globalRoomButton;

    public Button SettingsButton => _settingsButton;

    public RoomListPanelView RoomListPanel => _roomListPanel;

    public HomeLobbyView HomeLobbyViewPanel => _homeLobbyViewPanel;

    public SettingsMenuView SettingsMenuView => _settingsMenuView;
}