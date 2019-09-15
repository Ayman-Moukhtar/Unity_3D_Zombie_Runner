using Assets.Scripts;
using System.Collections;
using UnityEngine;

public enum ShootingMode
{
    Rifle, Pistol
}

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

    [SerializeField]
    private ShootingMode _shootingMode = ShootingMode.Rifle;

    private Animator _animator;

    private bool _isZoomedIn = false;

    private float _originalCameraFieldOfView;

    private WeaponAmmo _ammo;

    void Start()
    {
        _camera = Camera.main;
        _animator = GetComponent<Animator>();
        _ammo = GetComponent<WeaponAmmo>();
        _crosshairCanvas?.SetActive(true);
        _originalCameraFieldOfView = _camera.fieldOfView;
    }

    void Update()
    {
        if (Input.GetButtonDown(Constant.Button.SecondaryFire))
        {
            _isZoomedIn = !_isZoomedIn;
            _animator.SetBool(Constant.WeaponStateParameter.Zoomed, _isZoomedIn);
            return;
        }

        switch (_shootingMode)
        {
            case ShootingMode.Rifle:
                if (!Input.GetButton(Constant.Button.MainFire))
                {
                    _muzzleFlash.SetActive(false);
                    return;
                }
                break;
            case ShootingMode.Pistol:
                if (!Input.GetButtonDown(Constant.Button.MainFire))
                {
                    _muzzleFlash.SetActive(false);
                    return;
                }
                break;
            default:
                break;
        }

        Shoot();
    }

    private void Shoot()
    {
        if (_ammo?.GetRemainingBullets() == 0)
        {
            _muzzleFlash.SetActive(false);
            return;
        }

        _ammo?.Decrement();

        if (!_hasScope || (_hasScope && !_isZoomedIn))
        {
            _muzzleFlash.gameObject.SetActive(true);
        }

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, 10000f))
        {
            var damageableTarget = hit.collider.GetComponent<EnemyHealth>();
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

    // Called on Zoomed state start, animation event
    private void OnZoomedStateStart()
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
            GetComponent<MeshRenderer>().enabled = false;
        }
        _camera.fieldOfView = _camera.fieldOfView - _zoomFieldOfViewMargin;
    }

    private void InstantiateHitImpact(RaycastHit hit)
    {
        if (hit.point == null || hit.normal == null) return;
        var impact = Instantiate(_hitEffect, hit.point, Quaternion.LookRotation(hit.normal));
        Destroy(impact, 5f);
    }

    // Called by unity when object is set Inactive
    private void OnDisable()
    {
        if (_isZoomedIn)
        {
            _isZoomedIn = false;
            _animator.SetBool(Constant.WeaponStateParameter.Zoomed, _isZoomedIn);

            if (_hasScope)
            {
                _scopeCanvas?.SetActive(false);
                _camera.fieldOfView = _originalCameraFieldOfView;
            }
        }
    }

}
