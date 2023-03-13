using UnityEngine;

public sealed class InputUserModel
{ 
    public IUserInputAxis Horizontal { get; }
    public IUserInputAxis Vertical { get; }
    public IUserInputPosition MousePosition { get; }
    public IUserInputKeyPress Fire { get; }
    public IUserInputKeyPress Reload { get; }

    public InputUserModel(InputData inputData)
    {
        Horizontal = new PCInputAxis(AxisManager.HORIZONTAL);
        Vertical = new PCInputAxis(AxisManager.VERTICAL);
        MousePosition = new PCInputMousePosition();
        Fire = new PCInputKeyDown(inputData.Fire);
        Reload = new PCInputKeyDown(inputData.Reload);
    }
}