using Assets.Scripts.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IShootingTarget
{
    [SerializeField]
    private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
