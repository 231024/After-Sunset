using System;

public interface IUserInputKeyPress : IUserInput
{
    event Action OnKeyPressed;
    void GetKeyDown();   
}