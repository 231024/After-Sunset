using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class SupportObjectInitialization : IInitialization
{
    private readonly ISupportFactory _supportFactory;
    private readonly SupportObjectData _data;

    public SupportObjectInitialization(ISupportFactory supportFactory)
    {
        _supportFactory = supportFactory;
        _data = _supportFactory.Data;
        foreach (var dataListSupportObjectInfo in _data.ListSupportObjectInfos)
        {
            var supportObject = 
                _supportFactory.CreateSupportObject(dataListSupportObjectInfo.Type);
            supportObject.SetPositionAndRotation(new Vector3(0.0f, 0.01f, 0.0f), 
                Quaternion.Euler(90.0f, 0.0f, 0.0f));
        }
    }

    public void Initialization()
    {
    }
}