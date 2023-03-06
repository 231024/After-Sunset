using System.Collections.Generic;

internal sealed class EnemyInitialization : IInitialization
{
    private readonly IEnemyFactory _enemyFactory;
    private readonly EnemyData _data;
    private CompositeMove _enemy;
    private List<IEnemy> _enemies;

    public EnemyInitialization(IEnemyFactory enemyFactory)
    {
        _enemyFactory = enemyFactory;
        _data = _enemyFactory.Data;
        _enemy = new CompositeMove();
        _enemies = new List<IEnemy>();
        
        foreach (var dataListEnemyInfo in _data.ListEnemyInfos)
        {
            var enemy = _enemyFactory.CreateEnemy(dataListEnemyInfo.Type);
            _enemy.AddUnit(enemy);
            _enemies.Add(enemy);
            //Debug.Log(dataListEnemyInfo.Type);
        }
    }
        
    public void Initialization()
    {
    }

    public IMove GetMoveEnemies()
    {
        return _enemy;
    }

    public IEnumerable<IEnemy> GetEnemies()
    {
        foreach (var enemy in _enemies)
        {
            yield return enemy;
        }
    }
}