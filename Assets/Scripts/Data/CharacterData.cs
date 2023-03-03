using System;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitSettings", menuName = "Data/Unit/UnitSettings")]
public sealed class CharacterData : ScriptableObject, IUnit
{
    public GameObject Player;
    [SerializeField] private Transform _transformSpawn;
    [SerializeField, Range(0, 10)] private float _speed;
    //[SerializeField, Range(500, 1000)] private float _mouseSensitivity;
    [SerializeField] private DataRidgidbody _dataRidgidbody;
        
        
    public float Speed => _speed;
    //public float MouseSensitivity => _mouseSensitivity;
    public Transform TransformSpawn => _transformSpawn;
    public float Mass => _dataRidgidbody._mass;
    public float AngularDrag => _dataRidgidbody._angularDrag;
    public bool IsGravity => _dataRidgidbody._isGravity;
    public bool IsFreeze => _dataRidgidbody._isFreeze;
}
    
[Serializable]
struct DataRidgidbody
{
    [SerializeField, Range(0, 100)] internal float _mass;
    [SerializeField, Range(0, 10)] internal float _angularDrag;
    public bool _isGravity;
    public bool _isFreeze;
}