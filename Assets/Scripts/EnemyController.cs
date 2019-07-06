using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    [SerializeField]
    private float _chaseRange = 10f;

    private bool ShouldAttackTarget => CanSeeTarget;
    private bool CanSeeTarget => Vector3.Distance(transform.position, _target.position) <= _chaseRange;
    private bool InAttackRange => Vector3.Distance(transform.position, _target.position) <= _navMeshAgent.stoppingDistance;

    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!ShouldAttackTarget)
        {
            return;
        }

        if (InAttackRange)
        {
            Debug.Log("Attacking");
            return;
        }

        _navMeshAgent.SetDestination(_target.position);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _chaseRange);
    }
}

