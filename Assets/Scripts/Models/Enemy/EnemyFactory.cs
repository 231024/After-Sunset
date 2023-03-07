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
        return Object.Instantiate(enemyProvider);
    }
}