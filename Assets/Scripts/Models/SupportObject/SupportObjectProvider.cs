using System;
using UnityEngine;

public abstract class SupportObjectProvider : MonoBehaviour, ISupportObject, IComparable<SupportObjectProvider>
{
    
    
    public int CompareTo(SupportObjectProvider other)
    {
        return name.CompareTo(other.name);
    }
}