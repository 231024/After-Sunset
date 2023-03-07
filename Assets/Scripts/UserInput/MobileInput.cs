using System;
using UnityEngine;

internal sealed class MobileInput : IUserInputProxy<float>
{
    public event Action<float> AxisOnChange;
    public void GetAxis()
    {
        Debug.Log("нажали кнопку!");
    }
}