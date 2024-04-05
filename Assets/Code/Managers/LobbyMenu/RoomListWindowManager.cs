using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using VContainer.Unity;

internal sealed class RoomListWindowManager : IStartable, IDisposable
{
    private Button _buttonTabRoomList;
    private Button _buttonTabRoomCreate;
    private Button _buttonConnect;
    private Button _buttonRoomCreate;

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

        _buttonTabRoomList = _listRooomPanelView.RoomListButton;
        _buttonTabRoomCreate = _listRooomPanelView.CreateRoomButton;
        _buttonConnect = _listRooomPanelView.ConnectToRoom;
        _buttonRoomCreate = _createRoomPanelView.ButtonCreateRoom;

        _roomListPanelTransform = _listRooomPanelView.RoomListPanelTransform;
        _parentTransform = _listRooomPanelView.ParentTransformContent;
        _inputFieldRoomName = _listRooomPanelView.RoomNameInputField;

        _buttonTabRoomList.onClick.AddListener(SwitchRoomListInterfaces);
        _buttonTabRoomCreate.onClick.AddListener(SwitchRoomListInterfaces);
        _buttonRoomCreate.onClick.AddListener(ConnectToRoom);
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

    private void ConnectToRoom()
    {
        
    }

    public void Dispose()
    {
        _buttonTabRoomList.onClick.RemoveListener(SwitchRoomListInterfaces);
        _buttonTabRoomCreate.onClick.RemoveListener(SwitchRoomListInterfaces);
    }
}