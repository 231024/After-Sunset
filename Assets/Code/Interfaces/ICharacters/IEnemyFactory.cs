using UnityEngine;

public interface IEnemyFactory
{
    public EnemyData Data { get; }
    GameObject CreateEnemy(EnemyType type, Vector3 spawnPosition);
}