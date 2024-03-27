using MessagePipe;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class MainLifetimeScope : LifetimeScope
{
    [SerializeField] private GeneralViews _view;
    [SerializeField] private PhotonController _photon;


    [Inject] private readonly IPublisher<string, GeneralViews> _publisherView;
    [Inject] private readonly IPublisher<string, PhotonController> _publisherPhoton;


    protected override void Configure(IContainerBuilder builder)
    {
        var options = builder.RegisterMessagePipe();
        builder.RegisterMessageBroker<string, GeneralViews>(options);
        builder.RegisterMessageBroker<string, PhotonController>(options);
        
        _publisherView.Publish("GeneralView", _view);
        _publisherPhoton.Publish("PhotonController", _photon);

        builder.RegisterComponent(_view);
        builder.RegisterComponent(_photon);


        var bootstrapInitialization = new BootstrapInitialization(builder, _view, _photon);
    }
}
