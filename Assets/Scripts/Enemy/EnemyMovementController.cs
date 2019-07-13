using UnityEngine;
using UnityEngine.AI;
using static Assets.Scripts.Constant;

public class EnemyMovementController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _chaseRange = 10f;

    [SerializeField]
    private float _rotationSpeed = 5f;

    private bool _isProvoked = false;
    private bool ShouldAttackTarget => CanSeeTarget || _isProvoked;
    private bool CanSeeTarget => Vector3.Distance(transform.position, _target.position) <= _chaseRange;
    private bool InAttackRange => Vector3.Distance(transform.position, _target.position) <= _navMeshAgent.stoppingDistance;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!ShouldAttackTarget)
        {
            SetEnemyState(EnemyState.Idle);
            return;
        }

        if (InAttackRange)
        {
            LookAtTarget();
            Attack(true);
            return;
        }

        Attack(false);
        MoveTowardsTarget();
    }

    private void LookAtTarget()
    {
        // Normalized will ignore the magnitude and just return a vector with the direction
        var direction = (_target.position - transform.position).normalized;
        var targetRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * _rotationSpeed);
    }

    private void Attack(bool shouldAttack)
    {
        SetEnemyState(EnemyState.Attack, shouldAttack);
    }

    private void MoveTowardsTarget()
    {
        if (CanSeeTarget)
        {
            _isProvoked = false;
        }
        SetEnemyState(EnemyState.Move);
        _navMeshAgent.SetDestination(_target.position);
    }

    private void SetEnemyState(EnemyState state, bool booleanValue = false)
    {
        switch (state)
        {
            case EnemyState.Idle:
                _animator.SetTrigger(EnemyStateId.Idle);
                _animator.ResetTrigger(EnemyStateId.Move);
                break;
            case EnemyState.Move:
                _animator.SetTrigger(EnemyStateId.Move);
                _animator.SetBool(EnemyStateId.Attack, false);
                _animator.ResetTrigger(EnemyStateId.Idle);
                break;
            case EnemyState.Attack:
                _animator.SetBool(EnemyStateId.Attack, booleanValue);
                if (booleanValue)
                {
                    _animator.ResetTrigger(EnemyStateId.Move);
                }
                break;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }

    // Called by string reference
    private void OnDamageTaken()
    {
        _isProvoked = true;
    }
}

