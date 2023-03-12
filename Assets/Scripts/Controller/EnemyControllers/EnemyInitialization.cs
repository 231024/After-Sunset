//using System;
using System.Collections.Generic;
using UnityEngine;

internal sealed class EnemyInitialization : IInitialization, IFixedExecute
{
    private readonly IEnemyFactory _enemyFactory;
    private readonly EnemyData _data;
    private CompositeMove _enemy;
    private EnemySpawnPointView[] _spawnPositions;
    private List<IEnemy> _enemies;

    private float _timer;

    public EnemyInitialization(IEnemyFactory enemyFactory)
    {
        _spawnPositions = Object.FindObjectsOfType<EnemySpawnPointView>();
        _enemyFactory = enemyFactory;
        _data = _enemyFactory.Data;
        _enemy = new CompositeMove();
        _enemies = new List<IEnemy>();
        _timer = 0f;

        foreach (var dataListEnemyInfo in _data.ListEnemyInfos)
        {
            for (int i = 0; i < _data.SpawnInitializationCount; i++)
            {
                SpawnEnemy(dataListEnemyInfo);
            }
            
        }
    }

    public void Initialization()
    {
    }

    public void FixedExecute(float deltaTime)
    {
        if (_timer <= 0)
        {
            foreach (var dataListEnemyInfo in _data.ListEnemyInfos)
            {
                SpawnEnemy(dataListEnemyInfo);
            }
            
            _timer = _data.SpawnDelay;
        }
        _timer--;
    }

    public void SpawnEnemy(EnemyData.EnemyInfo enemyInfo)
    {
        var spawnPoint = _spawnPositions[Random.Range(0, _spawnPositions.Length)].transform.position;
        var spawnPosition = spawnPoint + Random.insideUnitSphere * _data.SpawnRadius;
        spawnPosition.y = spawnPoint.y;
        var enemy = _enemyFactory.CreateEnemy(enemyInfo.Type, spawnPosition);

        enemy.Unit.SetPatrolZone(_data);
        
        _enemy.AddUnit(enemy);
        _enemies.Add(enemy);
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