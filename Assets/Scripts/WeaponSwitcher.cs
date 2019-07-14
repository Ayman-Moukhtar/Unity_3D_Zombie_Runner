using Assets.Scripts;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(WeaponController))]
public class WeaponSwitcher : MonoBehaviour
{
    private List<WeaponController> _weapons;
    private List<string> _possibleEnumValues;
    private int _currentActiveWeapon = 1;

    private void Start()
    {
        _weapons = GetComponentsInChildren<WeaponController>(true).ToList();
        _possibleEnumValues = Enumerable
            .Range(1, _weapons.Count)
            .SelectMany(_ => new[] { $"Keypad{_}", $"Alpha{_}" })
            .ToList();
        SetActiveWeapon(_currentActiveWeapon);
    }

    private void Update()
    {
        ProcessScrollInput();
        ProcessKeyboardInput();
    }

    private void ProcessScrollInput()
    {
        if (Input.GetAxis(Constant.Button.ScrollWheel) > 0)
        {
            SetActiveWeapon(_currentActiveWeapon + 1);
            return;
        }

        if (Input.GetAxis(Constant.Button.ScrollWheel) < 0)
        {
            SetActiveWeapon(_currentActiveWeapon - 1);
        }

    }

    private void ProcessKeyboardInput()
    {
        for (int i = 0; i < _possibleEnumValues.Count; i++)
        {
            var possibleValue = _possibleEnumValues[i];
            Enum.TryParse(possibleValue, out KeyCode keyCode);
            if (Input.GetKeyDown(keyCode))
            {
                SetActiveWeapon(int.Parse(possibleValue.Substring(possibleValue.Length - 1)));
                return;
            }
        }
    }

    private void SetActiveWeapon(int num)
    {
        if (num > _weapons.Count)
        {
            num = 1;
        }

        if (num < 1)
        {
            num = _weapons.Count;
        }

        var weaponIndex = num - 1;
        _currentActiveWeapon = num;
        for (int i = 0; i < _weapons.Count; i++)
        {
            var weapon = _weapons[i];
            if (i == weaponIndex && !weapon.gameObject.activeSelf)
            {
                weapon.gameObject.SetActive(true);
                continue;
            }

            if (i != weaponIndex && weapon.gameObject.activeSelf)
            {
                weapon.gameObject.SetActive(false);
            }
        }
    }

    public WeaponController GetActiveWeapon() => _weapons.SingleOrDefault(_ => _.gameObject.activeSelf);
}
