using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WeaponPickup : MonoBehaviour
{
    public Weapon Weapon;
   
   
  [SerializeField] public GameObject NotifBox;
  [SerializeField] public GameObject NotifText;

    void Pickup()
    {
        WeaponManager.Instance.Add(Weapon);
        Destroy(gameObject);

        
    }

    private void OnMouseDown()
    {
        Pickup();
        Notif();
    }

     void Notif()
    {
        StartCoroutine(Notify());
    }

    IEnumerator Notify()
    {
        NotifText.GetComponent<TMP_Text>().text = "You have obtained a new weapon. Click Weapon Box to see.";
        yield return new WaitForSeconds(0.5f);
        NotifBox.SetActive(true);
        NotifText.SetActive(true);
        yield return new WaitForSeconds(4);
        NotifBox.SetActive(false);
        yield return null;
    }
    
}
