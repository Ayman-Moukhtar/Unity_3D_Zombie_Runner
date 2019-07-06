using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private Transform _target;

    private NavMeshAgent _navMeshAgent;

    void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        _navMeshAgent.SetDestination(_target.position);
    }
}
