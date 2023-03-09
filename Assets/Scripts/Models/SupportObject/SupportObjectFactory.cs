using UnityEngine;

public sealed class SupportObjectFactory : ISupportFactory
{
    public SupportObjectData Data { get; }

    public SupportObjectFactory(SupportObjectData data)
    {
        Data = data;
    }
    
    public Transform CreateSupportObject(SupportObjectType type)
    {
        var supportObject = Data.GetSupportObject(type);
        return new GameObject(type.ToString()).AddSupportObject(supportObject).transform;
    }
}