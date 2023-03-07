using System;
using UnityEngine;

public sealed class PCInputVertical : IUserInputProxy<float>
{
    public event Action<float> AxisOnChange = delegate {  };
        
    public void GetAxis()
    {
        AxisOnChange.Invoke(Input.GetAxis(AxisManager.VERTICAL));
    }
}