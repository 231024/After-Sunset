using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    private GeneralViews _view;
    private PhotonController _photon;

    protected override void Configure(IContainerBuilder builder)
    {
        _view = GetComponent<GeneralViews>();
        _photon = GetComponent<PhotonController>();
        
        builder.RegisterComponent(_view);
        builder.RegisterComponent(_photon);

        new BootstrapInitialization(builder);
    }
}
