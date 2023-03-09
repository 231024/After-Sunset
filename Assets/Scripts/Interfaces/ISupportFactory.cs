using UnityEngine;

public interface ISupportFactory
{
    public SupportObjectData Data { get; }
    Transform CreateSupportObject(SupportObjectType type);
}