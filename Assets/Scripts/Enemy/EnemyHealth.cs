using Assets.Scripts;
using Assets.Scripts.Interfaces;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _health = 600f;

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            BroadcastMessage(Constant.Event.OnEnemyHealthDepleted);
            return;
        }

        // Only broadcast to scripts that on the game object or its children
        BroadcastMessage(Constant.Event.OnDamageTaken);
    }
}
