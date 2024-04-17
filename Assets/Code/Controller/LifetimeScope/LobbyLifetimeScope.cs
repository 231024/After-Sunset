using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private LobbyGeneralViews _lobbyGeneralViews;
    
    protected override void Configure(IContainerBuilder builder)
    {

        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<LobbyGeneralViews>(options);
        builder.RegisterMessageBroker<string, string>(options);
        builder.RegisterComponent(_lobbyGeneralViews);


        builder.RegisterEntryPoint<LobbyWindowManager>().AsSelf();
        builder.RegisterEntryPoint<HeaderLobbyWindowManager>();
        builder.RegisterEntryPoint<LobbyController>();
        builder.RegisterEntryPoint<RoomListWindowManager>();
        builder.RegisterEntryPoint<CreateRoomWindowManager>();
        builder.RegisterEntryPoint<RoomInfoWindowManager>();
    }
}
