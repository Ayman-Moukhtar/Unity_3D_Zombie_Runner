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
    private Camera _fpsCamera;

    [SerializeField]
    private float _scopedFieldOfView = 20f;

    [SerializeField]
    private float _nonScopedFieldOfView = 60f;

    private Animator _animator;

    private bool _isScoped = false;
    // Start is called before the first frame update
    void Start()
    {
        _crosshairCanvas.SetActive(true);
        _camera = FindObjectOfType<Camera>();
        _animator = GetComponent<Animator>();
    }

    private IEnumerator Scope()
    {
        yield return new WaitForSeconds(.15f);
        _scopeCanvas.SetActive(_isScoped);
        _crosshairCanvas.SetActive(!_isScoped);
        GetComponent<MeshRenderer>().enabled = !_isScoped;

        _fpsCamera.fieldOfView = _isScoped ? _scopedFieldOfView : _nonScopedFieldOfView;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2"))
        {
            _isScoped = !_isScoped;
            _animator.SetBool("IsScoped", _isScoped);
            StartCoroutine(Scope());
            return;
        }

        if (!Input.GetButton("Fire1"))
        {
            _muzzleFlash.SetActive(false);
            return;
        }

        _muzzleFlash.gameObject.SetActive(true);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, 10000f))
        {
            var target = hit.collider.GetComponent<IDamageable>();
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
