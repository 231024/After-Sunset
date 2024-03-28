using System.Collections.Generic;


public sealed class Inputs : IExecute
{
    private readonly List<IUserInputAxis> _axises;
    private readonly List<IUserInputPosition> _positions;
    private readonly List<IUserInputKeyPress> _presses;

    public Inputs()
    {
        _axises = new List<IUserInputAxis>();
        _positions = new List<IUserInputPosition>();
        _presses = new List<IUserInputKeyPress>();
    }
    
    internal void Add(IUserInput input)
    {
        if (input is IUserInputKeyPress press)
            _presses.Add(press);

        if (input is IUserInputPosition position)
            _positions.Add(position);

        if (input is IUserInputAxis axisChange)
            _axises.Add(axisChange);
    }
    
    public void Execute(float deltaTime)
    {
        foreach (var press in _presses)
            press.GetKeyDown();
        foreach (var position in _positions)
            position.GetPosition();
        foreach (var axisChange in _axises)
            axisChange.GetAxis();
    }
}