using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

internal sealed class MainLifetimeScope : LifetimeScope
{
    private MainGeneralViews _view;
    private PhotonController _photon;

    protected override void Configure(IContainerBuilder builder)
    {
        _view = GetComponent<MainGeneralViews>();
        _photon = GetComponent<PhotonController>();
        
        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<string, string>(options);
        builder.RegisterComponent(_view);
        builder.RegisterComponent(_photon).AsSelf();

        new BootstrapInitialization(builder);
    }
}
