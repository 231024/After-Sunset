using UnityEngine;

internal sealed class InputInitialization : IInitialization
{
    private IUserInputProxy<float> _pcInputHorizontal;
    private IUserInputProxy<float> _pcInputVertical;
    private IUserInputProxy<float> _pcInputRotation;
    private readonly IUserInputProxy<Vector3> _pcInputMousePosition;

    public InputInitialization()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _pcInputHorizontal = new MobileInput();
        }
        _pcInputHorizontal = new PCInputHorizontal();
        _pcInputVertical = new PCInputVertical();
        _pcInputRotation = new PCInputRotation();
        _pcInputMousePosition = new PCInputMousePosition();

    }
        
    public void Initialization()
    {
    }

    public (IUserInputProxy<float> inputHorizontal, IUserInputProxy<float> inputVertical, 
        IUserInputProxy<float> inputRotation, IUserInputProxy<Vector3> inputMousePosition) GetInput()
    {
        (IUserInputProxy<float> inputHorizontal, IUserInputProxy<float> inputVertical, 
            IUserInputProxy<float> inputRotation, IUserInputProxy<Vector3> inputMousePosition) result = 
            (_pcInputHorizontal, _pcInputVertical, _pcInputRotation, _pcInputMousePosition);
        return result;
    }
}