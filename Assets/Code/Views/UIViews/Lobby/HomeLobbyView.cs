using TMPro;
using UnityEngine;
using UnityEngine.UI;

internal sealed class HomeLobbyView : MonoBehaviour
{
    [Header("Buttons")] 
    [SerializeField] private Button _buttonBack;
    [SerializeField] private Button _buttonLeaveRoom;
    [SerializeField] private Button _buttonStart;
    [SerializeField] private Button _buttonCloseRoom;
    [SerializeField] private Button _buttonInventory;
    [SerializeField] private Button _buttonCopy;


    [Header("Other Element")] 
    [SerializeField] private TMP_Text _inputFieldRoomName;
    [SerializeField] private Transform _contentListPlayers;

    public Button ButtonBack => _buttonBack;

    public Button ButtonLeaveRoom => _buttonLeaveRoom;

    public Button ButtonStart => _buttonStart;

    public Button ButtonCloseRoom => _buttonCloseRoom;

    public Button ButtonInventory => _buttonInventory;

    public TMP_Text InputFieldRoomName => _inputFieldRoomName;

    public Button ButtonCopy => _buttonCopy;

    public Transform ContentListPlayers => _contentListPlayers;
}
