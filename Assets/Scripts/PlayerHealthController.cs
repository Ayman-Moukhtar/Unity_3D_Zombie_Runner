using Assets.Scripts.Interfaces;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _health = 100f;

    public void TakeDamage(float damage)
    {
        _health -= damage;
        Debug.Log("Player Hit");

        if (_health <= 0f)
        {
            Debug.Log("Player Dead");
        }
    }
}
