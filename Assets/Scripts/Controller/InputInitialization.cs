using UnityEngine;

internal sealed class InputInitialization : IInitialization
{
    private IUserInputProxy _pcInputHorizontal;
    private IUserInputProxy _pcInputVertical;
    private IUserInputProxy _pcInputRotation;

    public InputInitialization()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            _pcInputHorizontal = new MobileInput();
        }
        _pcInputHorizontal = new PCInputHorizontal();
        _pcInputVertical = new PCInputVertical();
        _pcInputRotation = new PCInputRotation();
    }
        
    public void Initialization()
    {
    }

    public (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical, IUserInputProxy inputRotation) GetInput()
    {
        (IUserInputProxy inputHorizontal, IUserInputProxy inputVertical, IUserInputProxy inputRotation) result = 
            (_pcInputHorizontal, _pcInputVertical, _pcInputRotation);
        return result;
    }
}