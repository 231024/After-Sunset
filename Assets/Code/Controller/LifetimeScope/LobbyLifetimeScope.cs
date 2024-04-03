using UnityEngine;
using VContainer;
using VContainer.Unity;

public class LobbyLifetimeScope : LifetimeScope
{
    [SerializeField] private LobbyGeneralViews _lobbyGeneralViews;
    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_lobbyGeneralViews);
        
        builder.RegisterEntryPoint<ManagerLobbyWindowView>();
    }
}
