using UnityEngine;

public sealed class InputController : IExecute
    {
        //private readonly ISaveDataRepository _saveDataRepository;
        // private readonly IUserInputProxy<float> _horizontal;
        // private readonly IUserInputProxy<float> _vertical;
        // private readonly IUserInputProxy<float> _rotation;
        // private readonly IUserInputProxy<Vector3> _mousePosition;
        // private readonly IUserInputProxy<bool> _pcInputFire;
        private readonly Inputs _inputs;

        public InputController(InputUserModel inputModel)
            
            // (IUserInputProxy<float> inputHorizontal, 
            // IUserInputProxy<float> inputVertical, 
            // IUserInputProxy<float> inputRotation, 
            // IUserInputProxy<Vector3> mousePosition,
            // IUserInputProxy<bool> pcInputFire) input)
        {
            _inputs = new Inputs();
            
            _inputs.Add(inputModel.Horizontal);
            _inputs.Add(inputModel.Vertical);
            _inputs.Add(inputModel.MousePosition);
            _inputs.Add(inputModel.Fire);
            _inputs.Add(inputModel.Reload);
            // _horizontal = input.inputHorizontal;
            // _vertical = input.inputVertical;
            // _rotation = input.inputRotation;
            // _mousePosition = input.mousePosition;
            // _pcInputFire = input.pcInputFire;
        }

        public void Execute(float deltaTime)
        {
            _inputs.Execute(deltaTime);
            // _horizontal.GetPosition();
            // _vertical.GetPosition();
            // _rotation.GetPosition();
            // _mousePosition.GetPosition();
            // _pcInputFire.GetPosition();
        }
    }