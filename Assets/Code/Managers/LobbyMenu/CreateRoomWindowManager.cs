using System;
using MessagePipe;
using TMPro;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal sealed class CreateRoomWindowManager : IStartable, IDisposable
{
    private string _roomName;
    private float _amountMaxPlayers;
        
    private CreateRoomPanelView _createRoomPanelView;
    private LobbyController _lobbyController;
    private LobbyWindowManager _lobbyWindowManager;

    private TMP_InputField _roomNameTMPInputField;
    private Slider _maxPlayersSlider;
    private TMP_Text _maxPlayersTMPText;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    //[Inject] private LobbyController _photonController;
    [Inject] private PhotonController _photonController;
    [Inject] private readonly ISubscriber<LobbyWindowManager> _subscriber;
    private IDisposable _subscription;
    
    public void Start()
    {
        var sub = _subscriber.Subscribe(SetLobbyWindowManager);
        _subscription = DisposableBag.Create(sub);
        
        _createRoomPanelView = _lobbyGeneralViews.RoomListPanel.CreateRoomPanelView;
        _roomNameTMPInputField = _createRoomPanelView.RoomNameCreate;
        _maxPlayersSlider = _createRoomPanelView.AmountPlayerSlider;
        _maxPlayersTMPText = _createRoomPanelView.TextMaxPlayers;
        
        _roomNameTMPInputField.onValueChanged.AddListener(OnChangedRoomName);
        _maxPlayersSlider.onValueChanged.AddListener(OnChangedAmountMaxPayers);
        _createRoomPanelView.ButtonCreateRoom.onClick.AddListener(CreateRoom);
    }

    private void SetLobbyWindowManager(LobbyWindowManager roomListWindowManager)
    {
        _lobbyWindowManager = roomListWindowManager;
    }
    
    private void SetLobbyController(LobbyController lobbyController)
    {
        _lobbyController = lobbyController;
    }

    private void CreateRoom()
    {
        _photonController.CreateRoom(_roomName, _amountMaxPlayers, _createRoomPanelView.TogglePrivacy);
        _lobbyWindowManager.OpenRoomInfoPanel();
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

    public void Dispose()
    {
        _roomNameTMPInputField.onValueChanged.RemoveListener(OnChangedRoomName);
        _maxPlayersSlider.onValueChanged.RemoveListener(OnChangedAmountMaxPayers);
        _createRoomPanelView.ButtonCreateRoom.onClick.RemoveListener(CreateRoom);
        _subscription?.Dispose();
    }
}
