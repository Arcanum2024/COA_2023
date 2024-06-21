using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryWeaponController : MonoBehaviour
{
    public Weapon weapon;

    public Button RemoveButton;

    public void Remove()
    {
        WeaponManager.Instance.Remove(weapon);
        WeaponManager.Instance.ListWeapons();
        Destroy(gameObject);
    }

    public void Add(Weapon newWeapon)
    {
        weapon = newWeapon;
    }
    public void Equip()
    {
        if (weapon == null)
        {
            Debug.LogWarning("Item is null. Cannot use item.");
            return; // Exit the method if item is null
        }

        switch (weapon.weaponType)
        {
            case Weapon.WeaponType.Dagger:
                Player.Instance.UseDagger();
                break;
            case Weapon.WeaponType.Sword:
                Player.Instance.UseSword();
                break;
        }
        Remove();
    }

}
