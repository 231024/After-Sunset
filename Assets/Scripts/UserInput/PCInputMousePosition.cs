using System;
using UnityEngine;


public sealed class PCInputMousePosition : IUserInputProxy<Vector3>
{
    public event Action<Vector3> AxisOnChange = delegate {  };
        
    public void GetAxis()
    {
        AxisOnChange.Invoke(Input.mousePosition);
    }
}