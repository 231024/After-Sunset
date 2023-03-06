using System;
using UnityEngine;
using UnityEngine.AI;

public sealed class EnemyProvider : MonoBehaviour, IEnemy
{
    public event Action<int> OnTriggerEnterChange;

    [SerializeField] private float _speed;
    [SerializeField] private float _stopDistance;
    private int _takeDamage = 10;
    //private Rigidbody _rigidbody;
    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    //private Transform _transform;
    private IView _view;
    protected IHealth _health;

    private void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        // _rigidbody = GetComponent<Rigidbody>();
        // _transform = transform;
    }

    public void Move(Vector3 point)
    {
        transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
        _navMeshAgent.SetDestination(point);
        _animator.SetFloat("speed", _navMeshAgent.velocity.magnitude);
        // if ((_transform.localPosition - point).sqrMagnitude >= _stopDistance * _stopDistance)
        // {
        //     var dir = (point - _transform.localPosition).normalized;
        //     _navMeshAgent.velocity = dir * _speed;
        // }
        // else
        // {
        //     _navMeshAgent.velocity = Vector3.zero;
        // }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player"))
        {
            return;
        }
        OnTriggerEnterChange?.Invoke(_takeDamage);
        //_view.Display(_view.FirstKeyText, _health.PlayerHealth, _view.FirstText);
    }
        
    public void Initialization(IView view, IHealth health)
    {
        _view = view;
        _health = health;
    }
}