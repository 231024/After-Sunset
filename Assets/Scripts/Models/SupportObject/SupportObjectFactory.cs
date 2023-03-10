using UnityEngine;

public sealed class SupportObjectFactory : ISupportFactory
{
    public SupportObjectData Data { get; }

    public SupportObjectFactory(SupportObjectData data)
    {
        Data = data;
    }
    
    public ISupportObject CreateSupportObject(SupportObjectType type)
    {
        var supportObject = Data.GetSupportObject(type);
        return Object.Instantiate(supportObject);
        //return new GameObject(type.ToString()).AddSupportObject(supportObject);
    }
}