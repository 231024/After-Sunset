using System;
using UnityEngine;

public sealed class PCInputVertical : IUserInputProxy
{
    public event Action<float> AxisOnChange = delegate {  };
        
    public void GetAxis()
    {
        AxisOnChange.Invoke(Input.GetAxis(AxisManager.VERTICAL));
    }
}