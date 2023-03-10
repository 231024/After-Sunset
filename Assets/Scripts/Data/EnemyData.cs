using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "EnemySettings", menuName = "Data/Unit/EnemySettings")]
public sealed class EnemyData : ScriptableObject
{
    [SerializeField] private List<EnemyInfo> _enemyInfos;

    public List<EnemyInfo> ListEnemyInfos => _enemyInfos;

    [Serializable] 
    public struct EnemyInfo
    {
        public EnemyType Type;
        public EnemyProvider EnemyPrefab;
        public Transform Transform;
    }

    public EnemyProvider GetEnemy(EnemyType type)
    {
        var enemyInfo = _enemyInfos.First(info => info.Type == type);
        return enemyInfo.EnemyPrefab;
    }

    public Transform GetEnemyTransform(EnemyType type)
    {
        var enemyInfo = _enemyInfos.First(info => info.Type == type);
        return enemyInfo.Transform;
    }
}