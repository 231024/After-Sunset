using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public sealed class MoveController : IExecute, ICleanup
    {
        NavMeshAgent _navMeshAgent;
        private Animator _animator;
        public float _moveSpeed;
        
        private readonly Transform _unit;
        private GameObject _cursor;
        private readonly IUnit _unitData;
        private float _horizontal;
        private float _vertical;
        private Vector3 _move;
        private Vector3 _angelRotation;
        private List<ISupportObject> _supportObjects;
        private IUserInputAxis _horizontalInputProxy;
        private IUserInputAxis _verticalInputProxy;
        private bool _isAlive;


        public MoveController(InputUserModel input, Transform unit, IUnit unitData)//, IAlive isAlive)
        {
            _unit = unit;
            _unitData = unitData;
            _cursor = GameObject.Find(SupportObjectType.Cursor.ToString());
            _horizontalInputProxy = input.Horizontal;
            _verticalInputProxy = input.Vertical;
            _horizontalInputProxy.OnAxisChanged += HorizontalOnAxisOnChange;
            _verticalInputProxy.OnAxisChanged += VerticalOnAxisOnChange;
            //_isAlive = isAlive.IsAlive;
            _navMeshAgent = _unit.GetComponentInParent<NavMeshAgent>();
            _navMeshAgent.updateRotation = false;
            _animator = _unit.GetComponentInChildren<Animator>();
        }

        public void Execute(float deltaTime)
        {
            Vector3 forward;
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
            if (_cursor != null)
            {
                forward = _cursor.transform.position - _unit.position;
                _unit.transform.rotation = Quaternion.LookRotation(new Vector3(forward.x, 0, forward.z));
            }


        }

        private void VerticalOnAxisOnChange(float value)
        {
            _vertical = value;
        }

        private void HorizontalOnAxisOnChange(float value)
        {
            _horizontal = value;
        }

        public void Cleanup()
        {
            _horizontalInputProxy.OnAxisChanged -= HorizontalOnAxisOnChange;
            _verticalInputProxy.OnAxisChanged -= VerticalOnAxisOnChange;
        }
    }