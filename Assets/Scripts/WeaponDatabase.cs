using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDatabase : MonoBehaviour
{
    public TMP_Text weaponTitle, weaponDesc, weaponPrice;
    int weaponCounter = 0;
    public string[] names;
    public Image[] images;
    public GameManager gameManager;

    int amount;
    bool weaponBought = false;

    public class Weapons
    {
        public string title;
        public string description;
        public int price;
        public int damage;
    }

    public List<Weapons> availableWeapons = new List<Weapons>();
    public List<Weapons> collectedWeapons = new List<Weapons>();
    

    public WeaponDatabase() 
    {
        // moved weapon intialization to constructor
        // when switchin between scense the weapon database game object was loading later than game manager causing the data to not be loaded in time
        // the data loads before game manager only on game start
        // moving it here removes the issue
        Weapons electraCut = new Weapons
        {
            title = "ElectraCut",
            description = "DMG: 20\r\nSpecial: Lightining DMG\r\n\r\nCreated from the scales of the atlantic dragon LitBorne",
            damage = 20,
            price = 50

        };
        Weapons drogFire = new Weapons
        {
            title = "DrogFire",
            description = "DMG: 10\r\nSpecial: Fire DMG\r\n\r\nCreated from the scales of the atlantic dragon DrogBorne",
            damage = 10,
            price = 65

        };
        Weapons hammer = new Weapons
        {
            title = "Hammer",
            description = "DMG: 25\r\nSpecial: Power DMG\r\n\r\nCreated from the teeth of the dragon SolidBorne",
            damage = 25,
            price = 70
        };
        this.availableWeapons.Add(electraCut);
        this.availableWeapons.Add(drogFire);
        this.availableWeapons.Add(hammer);
        this.collectedWeapons.Add(electraCut);
    }


    void Start()
    {
        ShowWeaponData();
        ShowWeaponsCollected();
    }
    
    public void FindWeaponByTitle(string title)
    {
       foreach (var weapon in availableWeapons)
        {
            if (weapon.title == title)
            {
                weaponTitle.text = weapon.title;
                weaponDesc.text = weapon.description;
                // not needed for inventory
                //weaponPrice.text = weapon.price.ToString();
                
            }
        }
    }

    // for game manager to set the current equipped weapon along with its description
    public Weapons GetWeaponDetailsByTitle(string title)
    {
        foreach (var weapon in this.availableWeapons)
        {
            if (title == weapon.title)
            {
                return weapon;
            }
        }
        return null;
    }

    public void CollectWeapons(string weaponName)
    {
        Weapons weapon = availableWeapons.Find(w => w.title == weaponName);
        if(weapon != null)
        {
            this.collectedWeapons.Add(weapon);
        }
        else
        {

        }
    }
    public void ShowWeaponsCollected()
    {
        foreach (var weapon in this.collectedWeapons)
        {
            Debug.Log(weapon.title);
            for (int i = 0; i < names.Length; i++)
            {
                if (weapon.title == names[i])
                {
                    images[i].gameObject.SetActive(true);
                }
            }
        }
    }

    // function to show weapon details in weapons store
    public void ShowWeaponData()
    {
        Debug.Log("In show weapon data");
        if(weaponCounter < availableWeapons.Count)
        {
            weaponTitle.text = availableWeapons[weaponCounter].title;
            weaponDesc.text = availableWeapons[weaponCounter].description;
            weaponPrice.text = availableWeapons[weaponCounter].price.ToString();
            weaponCounter++;
            if(weaponCounter == availableWeapons.Count)
            {
                weaponCounter = 0;
            }
        }

    }
    public void CalculateWeaponTotalPrice()
    {
        amount = int.Parse(weaponPrice.text);
        weaponBought = gameManager.UpdateCoinsAfterPurchaseWeapon(amount);
        if (weaponBought)
        {
            foreach (var weapon in availableWeapons)
            {
                if (weapon.price == amount)
                {
                    this.collectedWeapons.Add(weapon);
                    ShowWeaponsCollected();
                }
            }
        }
        availableWeapons.RemoveAll(item => item.price == amount);
    }


}

