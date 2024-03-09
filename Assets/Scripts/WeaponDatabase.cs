using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WeaponDatabase : MonoBehaviour
{
    public TMP_Text weaponTitle, weaponDesc;
    public class Weapons
    {
        public string title;
        public string description;
    }

    public List<Weapons> availableWeapons = new List<Weapons>();
    public List<Weapons> collectedWeapons = new List<Weapons>();

    void Start()
    {
        Weapons electraCut = new Weapons
        {
            title = "ElectraCut",
            description = "DMG: 20\r\nSpecial: Lightining DMG\r\n\r\nCreated from the scales of the atlantic dragon LitBorne"
        };
        Weapons drogFire = new Weapons
        {
            title = "DrogFire",
            description = "DMG: 10\r\nSpecial: Fire DMG\r\n\r\nCreated from the scales of the atlantic dragon DrogBorne"
        };
        Weapons hammer = new Weapons
        {
            title = "Hammer",
            description = "DMG: 25\r\nSpecial: Power DMG\r\n\r\nCreated from the teeth of the dragon SolidBorne"
        };
        availableWeapons.Add(electraCut);
        availableWeapons.Add(drogFire);
        availableWeapons.Add(hammer);
    }
    public void FindWeaponByTitle(string title)
    {
       foreach (var weapon in availableWeapons)
        {
            if (weapon.title == title)
            {
                weaponTitle.text = weapon.title;
                weaponDesc.text = weapon.description;
            }
        }
    }
    public void CollectWeapons(string weaponName)
    {
        Weapons weapon = availableWeapons.Find(w => w.title == weaponName);
        if(weapon != null)
        {
            collectedWeapons.Add(weapon);
        }
        else
        {

        }
    }
    public void ShowWeaponsCollected()
    {
        foreach(var weapon in collectedWeapons) 
        {
            Debug.Log(weapon.title);
        }
    }
}
