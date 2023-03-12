using System;
using System.IO;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

//using Random = System.Random;

public sealed class EnemyProvider : MonoBehaviour, IEnemy
{
    public event Action<int> OnTriggerEnterChange;
    public EnemyProvider Unit => this;

    [SerializeField] private float _maxSpeedWalk;
    [SerializeField] private float _maxSpeedRun;
    [SerializeField] private float _stopDistance;
    [SerializeField] private int _takeDamage = 10;

    private NavMeshAgent _navMeshAgent;
    private NavMeshPath _navMeshPath;
    private NavMeshHit _navMeshHit;
    private Animator _animator;
    private CapsuleCollider _capsuleCollider;
    private Transform _transform;
    //private Transform _nearPlayerPoint;
    private Transform _patrolZoneCenter;
    private Vector3 _randomPoint;

    private IView _view;
    protected IHealth _health;

    private bool _dead;
    private bool _pathComplete = false;
    private bool _ray_blocked = false;
    private bool _islost = true;
    
    private static readonly int DiedState = Animator.StringToHash(GameConstants.ANIMATION_DIED);
    private static readonly int StateMove = Animator.StringToHash(GameConstants.ANIMATION_STATE_MOVE);
    private static readonly string SpeedState = GameConstants.ANIMATION_SPEED;

    private float _randomPointRadius;
    private float _nearPointRadius = 20.0f;
    private float _visibleRadius = 25.0f;

    private void Start()
    {
        _capsuleCollider = GetComponent<CapsuleCollider>();
        _animator = GetComponentInChildren<Animator>();
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.updateRotation = false;
        _navMeshPath = new NavMeshPath();
        _transform = transform;
        //_nearPlayerPoint = Instantiate(new GameObject(), Vector3.zero, Quaternion.identity).transform;
    }

    public void Initialization(IView view, IHealth health)
    {
        _view = view;
        _health = health;
    }

    public void Move(Vector3 point)
    {
        if (_dead) return;

        var vectorDirection = (_transform.localPosition - point).sqrMagnitude;
        _ray_blocked = NavMesh.Raycast(_transform.position, point,
            out _navMeshHit, NavMesh.AllAreas);

        if ((vectorDirection <= _visibleRadius * _visibleRadius && !_ray_blocked)
            || (vectorDirection <= _visibleRadius * _visibleRadius && _ray_blocked))
        {
            _islost = false;
            _pathComplete = false;
            
            Vector3 target = Vector3.zero;
             var nearVectorDirection = (target - point).magnitude;
             if (nearVectorDirection > _nearPointRadius * _nearPointRadius)
             {
                 var getCorrectPoint = false;
                 while (!getCorrectPoint)
                 {
                     NavMeshHit navMeshHit;
                     NavMesh.SamplePosition(Random.insideUnitSphere * _nearPointRadius + point,
                         out navMeshHit, _nearPointRadius, NavMesh.AllAreas);
                     target = navMeshHit.position;
            
                     _navMeshAgent.CalculatePath(target, _navMeshPath);
                     if (_navMeshPath.status == NavMeshPathStatus.PathComplete && !_ray_blocked)
                     {
                         getCorrectPoint = true;
                     }
                 }
             }
             else
             {
                 target = point;
                 _navMeshAgent.CalculatePath(target, _navMeshPath);
             }
            //var dir = (point - _transform.localPosition).normalized;
            
            
            _navMeshAgent.speed = Random.Range(_maxSpeedRun - 1, _maxSpeedRun);
            _navMeshAgent.avoidancePriority = Random.Range(40, 50);
            _navMeshAgent.SetDestination(target);
            _animator.SetInteger(StateMove, 2);
            _animator.SetFloat(SpeedState, _navMeshAgent.velocity.magnitude);
        }
        else if((_transform.localPosition - _randomPoint).magnitude <= 0.1f && _pathComplete)
        {
            NavMeshRandomPoint();
            _pathComplete = true;
        }
        else if (!_pathComplete && _islost)
        {
            NavMeshRandomPoint();
            _pathComplete = true;
        }
        else if (vectorDirection >= _visibleRadius * _visibleRadius)
        {
            _islost = true;
        }
        
        transform.rotation = Quaternion.LookRotation(_navMeshAgent.velocity.normalized);
    }

    private void NavMeshRandomPoint()
    {
        var getCorrectPoint = false;
        while (!getCorrectPoint)
        {
            NavMeshHit navMeshHit;
            NavMesh.SamplePosition(Random.insideUnitSphere * _randomPointRadius + _patrolZoneCenter.position,
                out navMeshHit, _randomPointRadius, NavMesh.AllAreas);
            _randomPoint = navMeshHit.position;

            _navMeshAgent.CalculatePath(_randomPoint, _navMeshPath);
            if (_navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                getCorrectPoint = true;
            }
        }

        _navMeshAgent.speed = Random.Range(_maxSpeedWalk - 1, _maxSpeedWalk);
        _navMeshAgent.SetDestination(_randomPoint);
        _animator.SetInteger(StateMove, 1);
        _animator.SetFloat(SpeedState, _navMeshAgent.velocity.magnitude);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(GameConstants.PLAYER))
        {
            return;
        }
        OnTriggerEnterChange?.Invoke(_takeDamage);
        //_view.Display(_view.FirstKeyText, _health.PlayerHealth, _view.FirstText);
    }

    public void SetPatrolZone(EnemyData enemyData)
    {
        _patrolZoneCenter = enemyData.PatrolZoneCenter;
        _randomPointRadius = enemyData.RandomPointRadius;
    }
    
    public void Kill()
    {
        if (!_dead) {
            _dead = true;
            Destroy(_capsuleCollider);
            Destroy(_navMeshAgent);
            GetComponentInChildren<ParticleSystem>().Play();
            _animator.SetTrigger(DiedState);
            Destroy(gameObject, 5);
        }
    }
}