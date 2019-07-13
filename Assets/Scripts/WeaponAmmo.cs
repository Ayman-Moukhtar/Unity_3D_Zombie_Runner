using UnityEngine;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField]
    private int _amount = 40;

    public int GetAmount() => _amount;

    public void Decrement() => _amount -= 1;
}
