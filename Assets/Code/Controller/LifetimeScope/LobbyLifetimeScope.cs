using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private LobbyGeneralViews _lobbyGeneralViews;
    private LobbyPhotonController _lobbyPhotonController;
    
    protected override void Configure(IContainerBuilder builder)
    {
        _lobbyPhotonController = GetComponent<LobbyPhotonController>();
        
        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<LobbyGeneralViews>(options);
        builder.RegisterComponent(_lobbyGeneralViews);
        
        builder.RegisterEntryPoint<ManagerLobbyWindowView>();
        builder.RegisterEntryPoint<RoomListWindowManager>();
    }
}
