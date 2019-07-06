using Assets.Scripts.Interfaces;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Camera _camera;

    [SerializeField]
    private float _damage = 30f;

    // Start is called before the first frame update
    void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!Input.GetButtonDown("Fire1"))
        {
            return;
        }

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, 300f))
        {
            var target = hit.collider.GetComponent<IShootingTarget>();
            target?.TakeDamage(_damage);
        }
    }
}
