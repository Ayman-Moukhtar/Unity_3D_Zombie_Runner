using Assets.Scripts;
using Assets.Scripts.Interfaces;
using System.Collections;
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

    [SerializeField]
    private GameObject _crosshairCanvas;

    [SerializeField]
    private GameObject _scopeCanvas;

    [SerializeField]
    private float _zoomFieldOfViewMargin = 40f;

    [SerializeField]
    private bool _hasScope = false;

    private Animator _animator;

    private bool _isZoomedIn = false;

    private float _originalCameraFieldOfView;

    void Start()
    {
        _camera = Camera.main;
        _originalCameraFieldOfView = _camera.fieldOfView;
        _animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown(Constant.Button.SecondaryFire))
        {
            _isZoomedIn = !_isZoomedIn;
            _animator.SetBool(Constant.WeaponStateParameter.Zoomed, _isZoomedIn);
            return;
        }

        if (!Input.GetButton(Constant.Button.MainFire))
        {
            _muzzleFlash.SetActive(false);
            return;
        }

        Shoot();
    }

    private void Shoot()
    {
        if (!_hasScope || (_hasScope && !_isZoomedIn))
        {
            _muzzleFlash.gameObject.SetActive(true);
        }

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, 10000f))
        {
            var damageableTarget = hit.collider.GetComponent<IDamageable>();
            damageableTarget?.TakeDamage(_damage);

            // To avoid making holes in the enemy
            if (damageableTarget != null) return;

            InstantiateHitImpact(hit);
        }
    }

    // Called on Idle state start, animation event
    private void OnIdleStateStart()
    {
        if (_hasScope)
        {
            _scopeCanvas?.SetActive(false);
        }
        _crosshairCanvas?.SetActive(true);
        _camera.fieldOfView = _originalCameraFieldOfView;

        GetComponent<MeshRenderer>().enabled = true;
    }

    // Called on Scoped state start, animation event
    private void OnScopedStateStart()
    {
        _crosshairCanvas?.SetActive(false);

        StartCoroutine(DoDelayedScopeWork());
    }

    private IEnumerator DoDelayedScopeWork()
    {
        yield return new WaitForSeconds(.15f);

        if (_hasScope)
        {
            _scopeCanvas?.SetActive(true);
        }
        _camera.fieldOfView = _camera.fieldOfView - _zoomFieldOfViewMargin;
        GetComponent<MeshRenderer>().enabled = false;
    }

    private void InstantiateHitImpact(RaycastHit hit)
    {
        if (hit.point == null || hit.normal == null) return;
        var impact = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 5f);
    }
}
