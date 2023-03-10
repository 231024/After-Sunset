using Unity.VisualScripting;
using UnityEngine;

public sealed class EnemyFactory : IEnemyFactory
{
    public EnemyData Data { get; }

    public EnemyFactory(EnemyData data)
    {
        Data = data;
    }
        
    public IEnemy CreateEnemy(EnemyType type)
    {
        var enemyProvider = Data.GetEnemy(type);
        var enemyTransform = Data.GetEnemyTransform(type);
        return Object.Instantiate(enemyProvider, enemyTransform.position, Quaternion.identity);
    }
}