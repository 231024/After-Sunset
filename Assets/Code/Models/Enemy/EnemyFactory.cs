using Photon.Pun;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class EnemyFactory : IEnemyFactory
{
    public EnemyData Data { get; }

    public EnemyFactory(EnemyData data)
    {
        Data = data;
    }
        
    public GameObject CreateEnemy(EnemyType type, Vector3 spawnPosition)
    {
        var enemyProvider = Data.GetEnemy(type);
        return PhotonNetwork.Instantiate(enemyProvider, spawnPosition, Quaternion.identity);
    }
}