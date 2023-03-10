﻿using UnityEngine;

public sealed class InputController : IExecute
    {
        //private readonly ISaveDataRepository _saveDataRepository;
        private readonly IUserInputProxy<float> _horizontal;
        private readonly IUserInputProxy<float> _vertical;
        private readonly IUserInputProxy<float> _rotation;
        private readonly IUserInputProxy<Vector3> _mousePosition;
        private readonly IUserInputProxy<float> _pcInputFire;

        public InputController((IUserInputProxy<float> inputHorizontal, 
            IUserInputProxy<float> inputVertical, 
            IUserInputProxy<float> inputRotation, 
            IUserInputProxy<Vector3> mousePosition,
            IUserInputProxy<float> pcInputFire) input)
        {
            _horizontal = input.inputHorizontal;
            _vertical = input.inputVertical;
            _rotation = input.inputRotation;
            _mousePosition = input.mousePosition;
            _pcInputFire = input.pcInputFire;
        }

        public void Execute(float deltaTime)
        {
            _horizontal.GetAxis();
            _vertical.GetAxis();
            _rotation.GetAxis();
            _mousePosition.GetAxis();
            _pcInputFire.GetAxis();
        }
    }