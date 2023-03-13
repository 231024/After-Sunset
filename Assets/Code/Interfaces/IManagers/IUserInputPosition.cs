using System;
using UnityEngine;

public interface IUserInputPosition : IUserInput
{
    event Action<Vector3> OnPositionChanged;
    void GetPosition(); 
}