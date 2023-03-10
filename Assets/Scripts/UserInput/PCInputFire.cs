using System;
using UnityEngine;

public class PCInputFire: IUserInputProxy<float>
{
    public event Action<float> AxisOnChange = delegate {  };
        
    public void GetAxis()
    {
        AxisOnChange.Invoke(Input.GetAxis(AxisManager.FIRE1));
    }
}