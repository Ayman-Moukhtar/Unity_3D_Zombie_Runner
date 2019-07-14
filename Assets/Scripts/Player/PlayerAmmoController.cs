using System;
using System.Linq;
using UnityEngine;
using static Assets.Scripts.Constant;

public class PlayerAmmoController : MonoBehaviour
{

    [Serializable]
    private class PlayerAmmoSlot
    {
        [SerializeField]
        private AmmoType _type = AmmoType.Rifle;

        [SerializeField]
        private int _capacity;

        public AmmoType GetAmmoType() => _type;

        public int RemainingBullets { get; private set; }

        private bool _initialized = false;

        public void Initialize()
        {
            if (_initialized) return;

            RemainingBullets = _capacity;
            _initialized = true;
        }

        public void AddAmmo(int amount)
        {
            if (amount > _capacity)
            {
                RemainingBullets = _capacity;
                return;
            }

            var slotsAvailable = _capacity - RemainingBullets;

            if (amount > slotsAvailable)
            {
                RemainingBullets = _capacity;
                return;
            }

            RemainingBullets = RemainingBullets + amount;
        }

        public int TakeAmmo(int amount)
        {
            if (RemainingBullets == 0)
            {
                return 0;
            }

            if (RemainingBullets >= amount)
            {
                RemainingBullets -= amount;
                return amount;
            }

            var newAmount = RemainingBullets;
            RemainingBullets = 0;

            return newAmount;
        }
    }

    [SerializeField]
    private PlayerAmmoSlot[] _ammoSlots;

    private void Start()
    {
        _ammoSlots.ToList().ForEach(_ => _.Initialize());
    }

    public void PickupAmmo(AmmoType type, int amount)
    {
        _ammoSlots.ToList()
            .First(_ => _.GetAmmoType() == type)?
            .AddAmmo(amount);
    }

    public int TakeAmmoFromSlot(AmmoType type, int amount)
    {
        return _ammoSlots.ToList()
            .First(_ => _.GetAmmoType() == type)?
            .TakeAmmo(amount) ?? 0;
    }
}
