using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private PlayerHealthController _target;

    [SerializeField]
    private float _damage = 20f;

    private void Start()
    {
        _target = FindObjectOfType<PlayerHealthController>();
    }

    // Called by string reference, Animation Event
    private void OnAttack()
    {
        if (_target == null) return;

        _target?.TakeDamage(_damage);
    }
}
