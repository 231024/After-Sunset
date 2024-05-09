using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemySettings", menuName = "Data/Unit/EnemySettings")]
public sealed class EnemyData : ScriptableObject
{
    [Header("List Enemies")]
    [SerializeField] private List<EnemyInfo> _enemyInfos;
    
    [Header("SpawmSettings")]
    [SerializeField] private float _spawnDelay;
    [SerializeField] private float _spawnInitializationCount;
    [SerializeField] private float _spawnRadius;

    [Header("NavMeshSettings")] 
    [SerializeField] private Transform _patrolZoneCenter;
    [SerializeField] private float _randomPointRadius;

    public List<EnemyInfo> ListEnemyInfos => _enemyInfos;
    public float SpawnDelay => _spawnDelay;

    public float SpawnInitializationCount => _spawnInitializationCount;

    public float SpawnRadius => _spawnRadius;

    public Transform PatrolZoneCenter => _patrolZoneCenter;

    public float RandomPointRadius => _randomPointRadius;

    [Serializable] 
    public struct EnemyInfo
    {
        public EnemyType Type;
        public string EnemyPrefab;
        public float Health;
    }

    public string GetEnemy(EnemyType type)
    {
        var enemyInfo = _enemyInfos.First(info => info.Type == type);
        return enemyInfo.EnemyPrefab;
    }
}