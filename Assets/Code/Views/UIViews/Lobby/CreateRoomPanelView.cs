using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class CreateRoomPanelView : MonoBehaviour
{
    [SerializeField] private TMP_InputField _roomNameCreate;
    [SerializeField] private Slider _amountPlayerSlider;
    [SerializeField] private TMP_Text _textMaxPlayers;
    [SerializeField] private Toggle _togglePrivacy;
    [SerializeField] private Button _buttonCreateRoom;

    public TMP_InputField RoomNameCreate => _roomNameCreate;

    public Slider AmountPlayerSlider => _amountPlayerSlider;

    public TMP_Text TextMaxPlayers => _textMaxPlayers;

    public Toggle TogglePrivacy => _togglePrivacy;

    public Button ButtonCreateRoom => _buttonCreateRoom;
}
