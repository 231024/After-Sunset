using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private LobbyGeneralViews _lobbyGeneralViews;
    private LobbyController _lobbyController;
    
    protected override void Configure(IContainerBuilder builder)
    {
        //_lobbyController = GetComponent<LobbyController>();


        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<LobbyGeneralViews>(options);
        builder.RegisterMessageBroker<LobbyController>(options);
        builder.RegisterMessageBroker<string, string>(options);
        builder.RegisterComponent(_lobbyGeneralViews);
        //builder.RegisterComponent(_lobbyController);

        builder.RegisterEntryPoint<ManagerLobbyWindowView>();
        builder.RegisterEntryPoint<LobbyController>();
        builder.RegisterEntryPoint<RoomListWindowManager>();
        builder.RegisterEntryPoint<CreateRoomWindowManager>();
    }
}
