using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class WeaponDatabase : MonoBehaviour
{
    public TMP_Text weaponTitle, weaponDesc, weaponPrice;
    public Image weaponStoreSprite;
    int weaponCounter = 0;
    int amount;
    bool weaponBought = false;

    public class Weapons
    {
        public string title;
        public string description;
        public int price;
        public int damage;
        public Sprite weaponSprite;

        public Weapons(string title,string description, int price, int damage)
        {
            this.title = title;
            this.description = description;
            this.price = price;
            this.damage = damage;
        }
    }

    public List<Weapons> availableWeapons = new List<Weapons>();
    public List<Weapons> collectedWeapons = new List<Weapons>();

    private Weapons electraCut, drogFire, hammer;

    public WeaponDatabase() 
    {
        // moved weapon intialization to constructor
        // when switchin between scense the weapon database game object was loading later than game manager causing the data to not be loaded in time
        // the data loads before game manager only on game start
        // moving it here removes the issue
        electraCut = new("ElectraCut", "DMG: 20\r\nSpecial: Lightining DMG\r\n\r\nCreated from the scales of the atlantic dragon LitBorne", 50, 20);
        drogFire = new("Sword Of Helios", "DMG: 10\r\nSpecial: Fire DMG\r\n\r\nThe sword made by Hephastus from the fragments of Helios (god of sun and light).", 65, 10);
        hammer = new("Hammer", "DMG: 25\r\nSpecial: Power DMG\r\n\r\nCreated from the teeth of the dragon SolidBorne", 70, 25);
        availableWeapons.Add(electraCut);
        availableWeapons.Add(drogFire);
        availableWeapons.Add(hammer);
    }


    void Start()
    {
        ShowWeaponData();
    }



    public void LoadWeaponSprites()
    {
        LoadWeaponSprite(drogFire);
        LoadWeaponSprite(electraCut);
        LoadWeaponSprite(hammer);
    }

    // loads the png file from resources folder and adds the sprite to the appropriate weapon object
    private void LoadWeaponSprite(Weapons weapon)
    {
      if(weapon != null)
        {
            if(weapon.title == "Sword Of Helios")
            {
                var sprite = Resources.Load<Sprite>("sword_1");
                weapon.weaponSprite= sprite;
            }
            else if(weapon.title == "ElectraCut")
            {
                var sprite = Resources.Load<Sprite>("sword_2");
                weapon.weaponSprite = sprite;
            }
            
            else if(weapon.title == "Hammer")
            {
                var sprite = Resources.Load<Sprite>("hammer");
                weapon.weaponSprite = sprite;
            }
            
        }
      

    }

    // for game manager to set the current equipped weapon along with its description
    public Weapons GetWeaponDetailsByTitle(string title)
    {
        foreach (var weapon in availableWeapons)
        {
            if (title == weapon.title)
            {
                return weapon;
            }
        }
        return null;
    }

    // currently unused. Will work when pick-up mechanic exists
    public void CollectWeapons(string weaponName)
    {
        Weapons weapon = availableWeapons.Find(w => w.title == weaponName);
        if(weapon != null)
        {
            collectedWeapons.Add(weapon);
        }
    }


    // function to show weapon details in weapons store
    public void ShowWeaponData()
    {
        if (availableWeapons.Count-1  == 0 || collectedWeapons.Count == 3)
        {
            weaponTitle.text = "Not Available";
            weaponDesc.text = "Not Available";
            weaponPrice.text = 0.ToString();
            weaponStoreSprite.sprite = null;
        }
        else if(weaponCounter < availableWeapons.Count)
        {
            Weapons weapon = collectedWeapons.Find(weapon => weapon.title == availableWeapons[weaponCounter].title);
            // to only show weapons not yet collected in the store
            if (weapon == null) 
            {

                weaponTitle.text = availableWeapons[weaponCounter].title;
                weaponDesc.text = availableWeapons[weaponCounter].description;
                weaponPrice.text = availableWeapons[weaponCounter].price.ToString();
                weaponStoreSprite.sprite = availableWeapons[weaponCounter].weaponSprite;
                weaponCounter++;
                if (weaponCounter == availableWeapons.Count)
                {
                    weaponCounter = 0;
                }
            }
            else
            {
                // skipping the next weapon as it is alreay in collected weapons in the next iteration
                weaponCounter++;
                weaponTitle.text = availableWeapons[weaponCounter].title;
                weaponDesc.text = availableWeapons[weaponCounter].description;
                weaponPrice.text = availableWeapons[weaponCounter].price.ToString();
                weaponStoreSprite.sprite = availableWeapons[weaponCounter].weaponSprite;
                weaponCounter++;
                if (weaponCounter == availableWeapons.Count)
                {
                    weaponCounter = 0;
                }
            }
        }

    }
    public void CalculateWeaponTotalPrice()
    {
        amount = int.Parse(weaponPrice.text);
        weaponBought = GameManager.instance.UpdateCoinsAfterPurchaseWeapon(amount);
        if (weaponBought)
        {
            foreach (var weapon in availableWeapons)
            {
                if (weapon.price == amount)
                {
                    collectedWeapons.Add(weapon);
                    // update the shop screen
                    ShowWeaponData();
                }
            }
        }
        availableWeapons.RemoveAll(item => item.price == amount);
        GameManager.instance.UpdateCoinsAndCollectedWeaponsAfterPurchase();
        GameInstance.instance.SaveGame();
    }


}

