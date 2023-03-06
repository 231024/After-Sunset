using UnityEngine;
using UnityEngine.AI;

public sealed class MoveController : IExecute, ICleanup
    {
        NavMeshAgent _navMeshAgent;
        private Animator _animator;
        public float _moveSpeed;
        
        private readonly Transform _unit;
        private readonly IUnit _unitData;
        private float _horizontal;
        private float _vertical;
        private float _rotation;
        private Vector3 _move;
        private Vector3 _angelRotation;
        private IUserInputProxy _horizontalInputProxy;
        private IUserInputProxy _verticalInputProxy;
        private IUserInputProxy _rotationInputProxy;
        private bool _isAlive;


        public MoveController((IUserInputProxy inputHorizontal, IUserInputProxy inputVertical, IUserInputProxy inputRotation) input,
            Transform unit, IUnit unitData)//, IAlive isAlive)
        {
            _unit = unit;
            _unitData = unitData;
            _horizontalInputProxy = input.inputHorizontal;
            _verticalInputProxy = input.inputVertical;
            _rotationInputProxy = input.inputRotation;
            _horizontalInputProxy.AxisOnChange += HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange += VerticalOnAxisOnChange;
            _rotationInputProxy.AxisOnChange += RotationOnAxisOnChange;
            //_isAlive = isAlive.IsAlive;
            _navMeshAgent = _unit.GetComponentInParent<NavMeshAgent>();
            _animator = _unit.GetComponentInChildren<Animator>();
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

        public void Execute(float deltaTime)
        {
            // Vector3 dir = Vector3.zero;
            // if (Input.GetKey(KeyCode.LeftArrow))
            //     dir.z = -1.0f;
            // if (Input.GetKey(KeyCode.RightArrow))
            //     dir.z = 1.0f;
            // if (Input.GetKey(KeyCode.UpArrow))
            //     dir.x = -1.0f;
            // if (Input.GetKey(KeyCode.DownArrow))
            //     dir.x = 1.0f;
            
            // if (_rotation != 0 && _isAlive)
            // {
            //     var sensitivity = _unitData.MouseSensitivity * deltaTime;
            //     _angelRotation.Set(0f, _rotation * sensitivity, 0f);
            //     _unit.transform.Rotate(_angelRotation);
            // }
            var speed = deltaTime * _unitData.Speed;
            _move.Set(_horizontal, 0.0f, _vertical);
            _navMeshAgent.velocity = _move.normalized * speed;
            _animator.SetFloat("speed", _navMeshAgent.velocity.magnitude);

            //_navMeshAgent.velocity = dir.normalized * _moveSpeed;
            //_unit.transform.Translate(_move);
        }

        public void Cleanup()
        {
            _horizontalInputProxy.AxisOnChange -= HorizontalOnAxisOnChange;
            _verticalInputProxy.AxisOnChange -= VerticalOnAxisOnChange;
            _rotationInputProxy.AxisOnChange -= RotationOnAxisOnChange;
        }
    }