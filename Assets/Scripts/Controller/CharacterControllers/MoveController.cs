using UnityEngine;
using UnityEngine.AI;

public sealed class MoveController : IExecute, ICleanup
    {
        NavMeshAgent _navMeshAgent;
        private Animator _animator;
        public float _moveSpeed;
        
        private readonly Transform _unit;
        private readonly Transform _cursor;
        private readonly IUnit _unitData;
        private float _horizontal;
        private float _vertical;
        private float _rotation;
        private Vector3 _mousePosition;
        private Vector3 _move;
        private Vector3 _angelRotation;
        private IUserInputProxy<float> _horizontalInputProxy;
        private IUserInputProxy<float> _verticalInputProxy;
        private IUserInputProxy<float> _rotationInputProxy;
        private IUserInputProxy<Vector3> _pcInputMousePosition;
        private bool _isAlive;


        public MoveController((IUserInputProxy<float> inputHorizontal, IUserInputProxy<float> inputVertical, 
                IUserInputProxy<float> inputRotation) input, Transform unit, IUnit unitData, Transform cursor)//, IAlive isAlive)
        {
            _unit = unit;
            _cursor = cursor;
            _unitData = unitData;
            _horizontalInputProxy = input.inputHorizontal;
            _verticalInputProxy = input.inputVertical;
            _rotationInputProxy = input.inputRotation;
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;
            _rotationInputProxy.AxisOnChange += RotationOnAxisOnChange;
            if (_pcInputMousePosition != null) _pcInputMousePosition.AxisOnChange += MousePositionOnAxisOnChange;
            //_isAlive = isAlive.IsAlive;
            _navMeshAgent = _unit.GetComponentInParent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _animator = _unit.GetComponentInChildren<Animator>();
        }

        public void Execute(float deltaTime)
        {
            // if (_rotation != 0 && _isAlive)
            // {
            //     var sensitivity = _unitData.MouseSensitivity * deltaTime;
            //     _angelRotation.Set(0f, _rotation * sensitivity, 0f);
            //     _unit.transform.Rotate(_angelRotation);
            // }
            var speed = deltaTime * _unitData.Speed;
            _move.Set(_horizontal, 0.0f, _vertical);
            _navMeshAgent.velocity = _move.normalized * speed;
            _animator.SetFloat(GameConstants.ANIMATION_SPEED, _navMeshAgent.velocity.magnitude);
            Vector3 forward = _cursor.transform.position - _unit.transform.position;
            _unit.transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));


        }

        private void MousePositionOnAxisOnChange(Vector3 value)
        {
            _mousePosition = value;
        }

        private void VerticalOnAxisOnChange(float value)
        {
            _vertical = value;
        }

        private void HorizontalOnAxisOnChange(float value)
        {
            _horizontal = value;
        }

        private void RotationOnAxisOnChange(float value)
        {
            _rotation = value;
        }

        public void Cleanup()
        {
            _horizontalInputProxy.AxisOnChange -= HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange -= VerticalOnAxisOnChange;
            _rotationInputProxy.AxisOnChange -= RotationOnAxisOnChange;
        }
    }