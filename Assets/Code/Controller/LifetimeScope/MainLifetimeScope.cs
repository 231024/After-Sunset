using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    [SerializeField] private GeneralViews _view;
    [SerializeField] private PhotonController _photon;



    protected override void Configure(IContainerBuilder builder)
    {
        builder.RegisterComponent(_view);
        builder.RegisterComponent(_photon);

        
        builder.RegisterEntryPoint<ManagerLoginWindow>();
        builder.RegisterEntryPoint<AnonymousAuthorization>();
        builder.RegisterEntryPoint<SignInAccountAuthorization>();
        builder.RegisterEntryPoint<CreateAccountAuthorization>();
    }
}