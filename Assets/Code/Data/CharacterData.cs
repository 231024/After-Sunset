using System;
using UnityEngine;
using UnityEngine.Serialization;


[CreateAssetMenu(fileName = "UnitSettings", menuName = "Data/Unit/UnitSettings")]
public sealed class CharacterData : ScriptableObject, IUnit
{
    public GameObject Player;
    [SerializeField] private Transform _transformSpawn;
    [SerializeField, Range(0, 1000)] private float _speed;
    [SerializeField, Range(500, 1000)] private float _mouseSensitivity;
    [SerializeField] private DataRidgidbody _dataRidgidbody;
    [SerializeField] private Transform _gunBarrelPosition;
    [SerializeField] private float _startHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _respawnTime;
    [SerializeField] private string[] _deathMessages;
    [SerializeField] private int _playerLayerId;
    [SerializeField] private int _scoreForKill;
    [SerializeField] private int _scoreForDeath;


    public Transform TransformSpawn => _transformSpawn;
    public Transform GunBarrelPosition => _gunBarrelPosition;
    public float Speed => _speed;
    public float MouseSensitivity => _mouseSensitivity;
    public float Mass => _dataRidgidbody._mass;
    public float AngularDrag => _dataRidgidbody._angularDrag;
    public bool IsGravity => _dataRidgidbody._isGravity;
    public bool IsFreeze => _dataRidgidbody._isFreeze;
    public float StartHealth => _startHealth;
    public float MaxHealth => _maxHealth;
    public float RespawnTime => _respawnTime;
    public string[] DeathMessages => _deathMessages;
    public int PlayerLayerId => _playerLayerId;
    public int ScoreForKill => _scoreForKill;
    public int ScoreForDeath => _scoreForDeath;
}
    
[Serializable]
struct DataRidgidbody
{
    [SerializeField, Range(0, 100)] internal float _mass;
    [SerializeField, Range(0, 10)] internal float _angularDrag;
    public bool _isGravity;
    public bool _isFreeze;
}