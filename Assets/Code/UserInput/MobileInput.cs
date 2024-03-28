using System;
using UnityEngine;

internal sealed class MobileInput : IUserInputAxis
{
    public event Action<float> OnAxisChanged;
    public void GetAxis()
    {
        
    }
}