using MessagePipe;
using VContainer;
using VContainer.Unity;

internal sealed class MainLifetimeScope : LifetimeScope
{
	private MainGeneralViews _view;

	protected override void Configure(IContainerBuilder builder)
	{
		_view = GetComponent<MainGeneralViews>();

		var options = builder.RegisterMessagePipe();
		builder.RegisterMessageBroker<string, string>(options);
		builder.RegisterComponent(_view);

		new BootstrapInitialization(builder);
	}
}
