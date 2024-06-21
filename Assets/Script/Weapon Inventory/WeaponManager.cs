using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WeaponManager : MonoBehaviour
{
    public static WeaponManager Instance;

    public List<Weapon> Weapons = new List<Weapon>();

    public Transform WeaponContent;
    public GameObject WeaponItem;
    public Toggle EnableRemove;
    public InventoryWeaponController[] WeaponItems;

    private void Awake()
    {
        Instance = this;
    }

    public void Add(Weapon weapon)
    {
        Weapons.Add(weapon);
        ListWeapons();
    }

    public void Remove(Weapon weapon)
    {
        Weapons.Remove(weapon);
        ListWeapons();
    }

    public void ListWeapons()
    {
        foreach (Transform weapon in WeaponContent)
        {
            Destroy(weapon.gameObject);
        }
        foreach(var weapon in Weapons)
        {
            GameObject obj = Instantiate(WeaponItem, WeaponContent);
            var weaponName = obj.transform.Find("WeaponName").GetComponent<TMPro.TextMeshProUGUI>();
            var weaponIcon = obj.transform.Find("WeaponIcon").GetComponent<Image>();
            var removeButton = obj.transform.Find("RemoveButton").GetComponent<Button>();
            
            weaponName.text = weapon.weaponName;
            weaponIcon.sprite = weapon.icon;

           if (EnableRemove.isOn)
                removeButton.gameObject.SetActive(true);
            else
                removeButton.gameObject.SetActive(false);

            obj.GetComponent<InventoryWeaponController>().Add(weapon); // Ensure each item is properly initialized
        
        }

        SetWeaponItems();
    }

    public void EnableWeaponRemove()
    {
            foreach (Transform weapon in WeaponContent)
            {
                weapon.Find("RemoveButton").gameObject.SetActive(true);
            }
    }

    public void SetWeaponItems()
    {
        WeaponItems = WeaponContent.GetComponentsInChildren<InventoryWeaponController>();

        for(int i = 0; i < Weapons.Count; i++)
        {
            WeaponItems[i].Add(Weapons[i]);
        }
    }
}
 