using Assets.Scripts.Interfaces;
using UnityEngine;

public class EnemyAttackController : MonoBehaviour
{
    private IDamageable _target;

    [SerializeField]
    private float _damage = 20f;

    private void Start()
    {
        _target = FindObjectOfType<PlayerHealthController>();
    }

    private void OnAttack()
    {
        if (_target == null) return;

        _target?.TakeDamage(_damage);
    }
}
