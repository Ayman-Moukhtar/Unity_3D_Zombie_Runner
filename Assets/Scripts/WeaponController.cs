using Assets.Scripts.Interfaces;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float _damage = 30f;

    [SerializeField]
    private GameObject _muzzleFlash;

    [SerializeField]
    private GameObject _hitEffect;

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButton("Fire1"))
        {
            _muzzleFlash.SetActive(false);
            return;
        }

        _muzzleFlash.gameObject.SetActive(true);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, 10000f))
        {
            var target = hit.collider.GetComponent<IShootingTarget>();
            target?.TakeDamage(_damage);

            // To avoid making holes in the enemy
            if (target != null) return;

            InstantiateHitImpact(hit);
        }
    }

    private void InstantiateHitImpact(RaycastHit hit)
    {
        if (hit.point == null || hit.normal == null) return;
        var impact = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 5f);
    }
}
