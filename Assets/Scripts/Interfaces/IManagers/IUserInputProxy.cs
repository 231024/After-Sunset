using System;

public interface IUserInputProxy<T>
{
    event Action<T> AxisOnChange;
    void GetAxis();
}