using Assets.Scripts.Interfaces;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _health = 100f;

    private PlayerDeathHandler _deathHandler;

    private void Start()
    {
        _deathHandler = GetComponent<PlayerDeathHandler>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0f)
        {
            _deathHandler.Die();
        }
    }
}
