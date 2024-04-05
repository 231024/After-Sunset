using System;
using TMPro;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal sealed class CreateRoomWindowManager : IStartable, IDisposable
{
    private string _roomName;
    private float _amountMaxPlayers;
        
    private CreateRoomPanelView _createRoomPanelView;

    private TMP_InputField _roomNameTMPInputField;
    private Slider _maxPlayersSlider;
    private TMP_Text _maxPlayersTMPText;
    
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;
    [Inject] private LobbyPhotonController _photonController;
    
    public void Start()
    {
        _createRoomPanelView = _lobbyGeneralViews.RoomListPanel.CreateRoomPanelView;
        _roomNameTMPInputField = _createRoomPanelView.RoomNameCreate;
        _maxPlayersSlider = _createRoomPanelView.AmountPlayerSlider;
        _maxPlayersTMPText = _createRoomPanelView.TextMaxPlayers;
        
        _roomNameTMPInputField.onValueChanged.AddListener(OnChangedRoomName);
        _maxPlayersSlider.onValueChanged.AddListener(OnChangedAmountMaxPayers);
        _createRoomPanelView.ButtonCreateRoom.onClick.AddListener(CreateRoom);
    }

    private void CreateRoom()
    {
        _photonController.CreateRoom(_roomName, _amountMaxPlayers, _createRoomPanelView.TogglePrivacy);
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
    }
}
