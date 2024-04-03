using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal sealed class RoomListWindowManager : IStartable, IDisposable
{
    private Button _buttonRoomList;
    private Button _buttonRoomCreate;
    private Button _buttonConnect;

    private TMP_InputField _inputFieldRoomName;

    private Transform _roomListPanelTransform;
    private Transform _parentTransform;
    
    private RoomListPanelView _listRooomPanelView;
    private CreateRoomPanelView _createRoomPanelView;
    
    [Inject] private LobbyGeneralViews LobbyGeneralViews;
    
    public void Start()
    {
        _listRooomPanelView = LobbyGeneralViews.RoomListPanel;
        _createRoomPanelView = _listRooomPanelView.CreateRoomPanelView;

        _buttonRoomList = _listRooomPanelView.RoomListButton;
        _buttonRoomCreate = _listRooomPanelView.CreateRoomButton;
        _buttonConnect = _listRooomPanelView.ConnectToRoom;

        _roomListPanelTransform = _listRooomPanelView.RoomListPanelTransform;
        _parentTransform = _listRooomPanelView.ParentTransformContent;
        _inputFieldRoomName = _listRooomPanelView.RoomNameInputField;

        _buttonRoomList.onClick.AddListener(SwitchRoomListInterfaces);
        _buttonRoomCreate.onClick.AddListener(SwitchRoomListInterfaces);
    }

    private void SwitchRoomListInterfaces()
    {
        if (!_roomListPanelTransform.gameObject.activeSelf)
        {
            _createRoomPanelView.gameObject.SetActive(false);
            _roomListPanelTransform.gameObject.SetActive(true);
        }
        else
        {
            _roomListPanelTransform.gameObject.SetActive(false);
            _createRoomPanelView.gameObject.SetActive(true);
        }
    }

    public void Dispose()
    {
        
    }
}