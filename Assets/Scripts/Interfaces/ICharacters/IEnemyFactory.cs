public interface IEnemyFactory
{
    public EnemyData Data { get; }
    IEnemy CreateEnemy(EnemyType type);
}