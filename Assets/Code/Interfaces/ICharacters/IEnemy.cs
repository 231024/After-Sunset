using System;
using UnityEngine;

public interface IEnemy : IMove
{
    event Action<int> OnTriggerEnterChange;
    EnemyProvider Unit { get; }
    void Initialization(IView view, IHealth health);
}