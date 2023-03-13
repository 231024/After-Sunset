using System;
using UnityEngine;

public abstract class SupportObjectProvider : MonoBehaviour, ISupportObject, IComparable<SupportObjectProvider>
{
    public GameObject SupportGameObject { get; set; }
    protected SupportObjectData _supportObjectData;

    public event Action<int> OnTriggerEnterChange;

    public void Initialization(SupportObjectData supportObjectData)
    {
        SupportGameObject = gameObject;
        _supportObjectData = supportObjectData;
    }

    public int CompareTo(SupportObjectProvider other)
    {
        return name.CompareTo(other.name);
    }
}