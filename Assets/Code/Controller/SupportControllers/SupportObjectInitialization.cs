using System.Collections.Generic;

public class SupportObjectInitialization : IInitialization
{
    private readonly ISupportFactory _supportFactory;
    private readonly SupportObjectData _data;
    private List<ISupportObject> _supportObjects;

    public List<ISupportObject> SupportObjects => _supportObjects;

    public SupportObjectInitialization(ISupportFactory supportFactory)
    {
        _supportFactory = supportFactory;
        _data = supportFactory.Data;
        _supportObjects = new List<ISupportObject>();
        foreach (var dataListSupportObjectInfo in _data.ListSupportObjectInfos)
        {
            var supportObject = 
                _supportFactory.CreateSupportObject(dataListSupportObjectInfo.Type);
            supportObject.Initialization(_data);
            supportObject.SupportGameObject.name = dataListSupportObjectInfo.Type.ToString();
            _supportObjects.Add(supportObject);
        }
    }

    public IEnumerable<ISupportObject> GetSupportObject()
    {
        foreach (var supportObject in _supportObjects)
        {
            yield return supportObject;
        }
    }

    public void Initialization()
    {
    }
}