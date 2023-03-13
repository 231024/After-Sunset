using System;
using UnityEngine;

public interface ISupportObject
{
    public GameObject SupportGameObject { get; set; }
    void Initialization(SupportObjectData supportObjectData);
}