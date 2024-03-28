using System.Collections.Generic;

namespace Code
{
    public class EnemyController : IController
    {
        public EnemyController(IEnumerable<IEnemy> getEnemies, IView view, IHealth health)
        {
            foreach (var enemy in getEnemies)
            {
                enemy.Initialization(view, health);
            }
        }
    }
}