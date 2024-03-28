using System;
using UnityEngine;


public sealed class PCInputMousePosition : IUserInputPosition
{
    public event Action<Vector3> OnPositionChanged = delegate {  };

    public void GetPosition()
    {
        OnPositionChanged.Invoke(Input.mousePosition);
    }
}