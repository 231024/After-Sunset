using System;

public interface IEnemy : IMove
{
    event Action<int> OnTriggerEnterChange;
    void Initialization(IView view, IHealth health);
}