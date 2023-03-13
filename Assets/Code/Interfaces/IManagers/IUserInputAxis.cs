using System;

public interface IUserInputAxis : IUserInput
{
    event Action<float> OnAxisChanged;
    void GetAxis(); 
}