using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

internal sealed class RoomListPanelView : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button _roomListButton;
    [SerializeField] private Button _createRoomButton;
    [SerializeField] private Button _connectToRoom;

    [Header("Other Elements")]
    [SerializeField] private Transform _parentTransformContent;
    [SerializeField] private TMP_InputField _roomNameInputField;

    [Header("Panels")] 
    [SerializeField] private CreateRoomPanelView _createRoomPanelView;
    [SerializeField] private Transform _roomListPanelTransform;


    public Button RoomListButton => _roomListButton;
    public Button CreateRoomButton => _createRoomButton;
    public Button ConnectToRoom => _connectToRoom;
    public Transform ParentTransformContent => _parentTransformContent;
    public TMP_InputField RoomNameInputField => _roomNameInputField;
    public CreateRoomPanelView CreateRoomPanelView => _createRoomPanelView;
    public Transform RoomListPanelTransform => _roomListPanelTransform;
}
