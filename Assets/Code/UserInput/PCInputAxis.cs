using System;
using UnityEngine;


public sealed class PCInputAxis : IUserInputAxis
{
    public event Action<float> OnAxisChanged;

    private readonly string _axis;

    public PCInputAxis(string axis)
    {
        _axis = axis;
    }
    
    public void GetAxis()
    {
        OnAxisChanged.Invoke(Input.GetAxis(_axis));
    }
}