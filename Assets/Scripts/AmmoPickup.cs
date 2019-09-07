using UnityEngine;
using static Assets.Scripts.Constant;

public class AmmoPickup : MonoBehaviour
{
    [SerializeField]
    private AmmoType _ammoType = AmmoType.Rifle;

    [SerializeField]
    private int _amount = 120;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != Tag.Player)
        {
            return;
        }

        other.gameObject
            .GetComponent<PlayerAmmoController>()
            .PickupAmmo(_ammoType, _amount);

        Destroy(gameObject);
    }
}
