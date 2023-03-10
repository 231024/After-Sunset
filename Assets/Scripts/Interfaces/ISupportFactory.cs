using UnityEngine;

public interface ISupportFactory
{
    public SupportObjectData Data { get; }
    ISupportObject CreateSupportObject(SupportObjectType type);
}