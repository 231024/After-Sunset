using UnityEngine;
using UnityEngine.UI;

public sealed class ManagerLobbyWindowView : MonoBehaviour
{
    [Header("Global UI Elements")]
    [SerializeField] private Button _homeButton;
    [SerializeField] private Button _globalRoomButton;
    [SerializeField] private Button _settingsButton;

    [Header("Children Views")] 
    [SerializeField] private Transform _homeMenuLobby;
    [SerializeField] private Transform _roomListPanel;


    public Button HomeButton => _homeButton;
    public Button GlobalRoomButton => _globalRoomButton;
    public Button SettingsButton => _settingsButton;
}