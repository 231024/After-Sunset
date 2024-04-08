using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class ProjectLifetimeScope : LifetimeScope
{
	[SerializeField] private PhotonController _photon;

	protected override void Configure(IContainerBuilder builder)
	{
		var options = builder.RegisterMessagePipe();
		builder.RegisterMessageBroker<string, string>(options);

		builder.RegisterComponent(_photon);
	}
}
