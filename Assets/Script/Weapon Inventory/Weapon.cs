using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon", menuName = "Weapon/Create New Weapon")]
public class Weapon : ScriptableObject
{
    public int id;
    public string weaponName;
    public int value;
    public Sprite icon;

    public WeaponType weaponType;

    public enum WeaponType
    {
        Dagger,
        Sword

    }

}
