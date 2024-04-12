using VContainer;
using VContainer.Unity;

internal sealed class MainLifetimeScope : LifetimeScope
{
	private MainGeneralViews _view;

	protected override void Configure(IContainerBuilder builder)
	{
		_view = GetComponent<MainGeneralViews>();

		builder.RegisterComponent(_view);
		builder.RegisterEntryPoint<ManagerLoginWindow>(Lifetime.Singleton);
		builder.RegisterEntryPoint<AnonymousAuthorization>(Lifetime.Singleton);
		builder.RegisterEntryPoint<SignInAccountAuthorization>(Lifetime.Singleton);
		builder.RegisterEntryPoint<CreateAccountAuthorization>(Lifetime.Singleton);

		//new BootstrapInitialization(builder);
	}
}
