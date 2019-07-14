using UnityEngine;

public class WeaponReloader : MonoBehaviour
{
    private void Update()
    {
        if (!Input.GetKeyDown(KeyCode.R))
        {
            return;
        }

        var activeWeaponAmmo = GetComponent<WeaponSwitcher>()?
            .GetActiveWeapon()?
            .GetComponent<WeaponAmmo>();

        if (activeWeaponAmmo == null)
        {
            return;
        }

        if (activeWeaponAmmo.IsFull())
        {
            return;
        }

        var ammoType = activeWeaponAmmo.GetAmmoType();
        var numberOfNeededBullets = activeWeaponAmmo.GetNumberOfNeededBullets();
        var availableAmmo = GetComponent<PlayerAmmoController>().TakeAmmoFromSlot(ammoType, numberOfNeededBullets);

        if (availableAmmo == 0)
        {
            return;
        }

        activeWeaponAmmo.Reload(availableAmmo);
    }
}
