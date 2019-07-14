using UnityEngine;
using static Assets.Scripts.Constant;

public class WeaponAmmo : MonoBehaviour
{
    [SerializeField]
    private AmmoType _type = AmmoType.Rifle;

    [SerializeField]
    private int _capacity = 40;

    private int _remainingBullets;

    private void Start() => _remainingBullets = _capacity;

    public int GetRemainingBullets() => _remainingBullets;

    public void Decrement()
    {
        if (_remainingBullets == 0)
        {
            return;
        }

        _remainingBullets -= 1;
    }

    public AmmoType GetAmmoType() => _type;
    public int GetCapacity() => _capacity;
    public bool IsFull() => _capacity == _remainingBullets;
    public int GetNumberOfNeededBullets() => _capacity - _remainingBullets;

    public void Reload(int amount)
    {
        if (amount > _capacity)
        {
            _remainingBullets = _capacity;
            return;
        }

        var neededBullets = GetNumberOfNeededBullets();
        if (amount > neededBullets)
        {
            _remainingBullets = _capacity;
            return;
        }

        _remainingBullets = _remainingBullets + amount;
    }
}
