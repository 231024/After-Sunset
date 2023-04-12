internal sealed class BootstrapInitialization
{
    public BootstrapInitialization(Controllers controllers, GeneralViews views)
    {
        //var anonymousAuthorization = new AnonymousAuthorization(views);

        controllers.Add(new AnonymousAuthorization(views));
        controllers.Add(new SignInAccountAuthorization(views));
        controllers.Add(new CreateAccountAuthorization(views));
    }
}