using VContainer;
using VContainer.Unity;

internal sealed class BootstrapInitialization 
{
    public BootstrapInitialization(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<ManagerLoginWindow>(Lifetime.Singleton);
        builder.RegisterEntryPoint<AnonymousAuthorization>(Lifetime.Singleton);
        builder.RegisterEntryPoint<SignInAccountAuthorization>(Lifetime.Singleton);
        builder.RegisterEntryPoint<CreateAccountAuthorization>(Lifetime.Singleton);
        
        //var anonymousAuthorization = new AnonymousAuthorization(views);

        // controllers.Add(new ManagerLoginWindow(views.ManagerLoginWindowView));
        // controllers.Add(new AnonymousAuthorization(views));
        // controllers.Add(new SignInAccountAuthorization(views));
        // controllers.Add(new CreateAccountAuthorization(views));
    }
}