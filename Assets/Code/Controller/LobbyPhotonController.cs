using System;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using VContainer;

public class LobbyPhotonController : MonoBehaviourPunCallbacks
{
    protected const string DEFAULT_MAP_NAME = "Default";
        
    [Inject] private LobbyGeneralViews _lobbyGeneralViews;

    private void Start()
    {  
        Debug.Log($"Connect server in Scene Lobby = {PhotonNetwork.IsConnected}");
    }

    // public override void OnRoomListUpdate(List<RoomInfo> roomList)
    // {
    //     LogFeedback("OnRoomListUpdate");
    //     var roomListPanel = _lobbyGeneralViews.RoomListPanel;
    //
    //     foreach (var roomInfo in roomList)
    //     {
    //         var itemRoomInfo = Instantiate(Resources.Load<GameObject>(UIConstants.INFO_ROOM_ITEM_PREFAB), roomListPanel.ParentTransformContent);
    //         var view = itemRoomInfo.GetComponent<InfoRoomItemView>();
    //         view.LabelRoomName.text = roomInfo.Name;
    //         view.LabelAmountPlayers.text = roomInfo.PlayerCount.ToString();
    //         view.LabelMapName.text = DEFAULT_MAP_NAME;
    //     }
    // }
}