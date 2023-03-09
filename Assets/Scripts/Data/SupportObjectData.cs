using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SupportObjectSettings", menuName = "Data/Unit/SupportObjectSettings")]
public sealed class SupportObjectData : ScriptableObject
{
    [SerializeField] private List<SupportObjectInfo> _supportObjectInfos;

    public List<SupportObjectInfo> ListSupportObjectInfos => _supportObjectInfos;

    [Serializable] 
    public struct SupportObjectInfo
    {
        public SupportObjectType Type;
        public GameObject SupportObjectPrefab;
        public Transform _position;
    }

    public GameObject GetSupportObject(SupportObjectType type)
    {
        var interactiveObjectInfo = _supportObjectInfos.First(info => info.Type == type);
        return interactiveObjectInfo.SupportObjectPrefab;
    }
}