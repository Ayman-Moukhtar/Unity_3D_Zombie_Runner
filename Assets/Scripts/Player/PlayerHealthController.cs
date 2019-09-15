using System.Collections;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    [SerializeField]
    private float _health = 100f;

    [SerializeField]
    private Canvas _damageReceivedCanvas;

    [SerializeField]
    private float _damageEffectDuration = .3f;

    private PlayerDeathHandler _deathHandler;

    private void Start()
    {
        _deathHandler = GetComponent<PlayerDeathHandler>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        ShowDamageEffect();

        if (_health <= 0f)
        {
            _deathHandler.Die();
        }
    }

    private void ShowDamageEffect()
    {
        StartCoroutine(DoShowDamageEffect());

        IEnumerator DoShowDamageEffect()
        {
            _damageReceivedCanvas.gameObject.SetActive(true);
            yield return new WaitForSeconds(_damageEffectDuration);

            if (_health <= 0)
            {
                StopCoroutine(DoShowDamageEffect());
            }

            _damageReceivedCanvas.gameObject.SetActive(false);
        }
    }
}
