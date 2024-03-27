using VContainer;
using VContainer.Unity;

internal sealed class BootstrapInitialization 
{
    public BootstrapInitialization(IContainerBuilder builder, GeneralViews views, PhotonController photon)
    {
        builder.RegisterEntryPoint<ManagerLoginWindow>();
        builder.RegisterEntryPoint<AnonymousAuthorization>();
        builder.RegisterEntryPoint<SignInAccountAuthorization>();
        builder.RegisterEntryPoint<CreateAccountAuthorization>();
        
        //var anonymousAuthorization = new AnonymousAuthorization(views);

        // controllers.Add(new ManagerLoginWindow(views.ManagerLoginWindowView));
        // controllers.Add(new AnonymousAuthorization(views));
        // controllers.Add(new SignInAccountAuthorization(views));
        // controllers.Add(new CreateAccountAuthorization(views));
    }
}