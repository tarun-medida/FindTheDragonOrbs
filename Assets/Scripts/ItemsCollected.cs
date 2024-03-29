using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static WeaponDatabase;

public class ItemsCollected : MonoBehaviour
{
    public GameObject[] ItemDataObject;
    public WeaponDatabase weaponDatabase;

    public TMP_Text weaponTitle, WeaponDesc;

    private int collectedWeaponsCounter = 0;

    // Start is called before the first frame update

    // loading the collected weapons

    private void Start()
    {
        collectedWeaponsCounter = weaponDatabase.collectedWeapons.Count;
        UpdateCollectedWeapons();
        
    }

    void FixedUpdate()
    {
        if(collectedWeaponsCounter != weaponDatabase.collectedWeapons.Count)
        {
            UpdateCollectedWeapons();
            collectedWeaponsCounter = weaponDatabase.collectedWeapons.Count;
        }
    }


    // Show Collected Weapon Description On Load
    public void ViewItemDescription(ItemData item)
    {
        weaponTitle.text = item.title;
        WeaponDesc.text = item.itemDesc;

    }

    private void UpdateCollectedWeapons()
    {
        int i = 0;
        foreach (var weapon in weaponDatabase.collectedWeapons)
        {

            ItemDataObject[i].gameObject.SetActive(true);
            ItemDataObject[i].GetComponent<ItemData>().title = weapon.title;
            ItemDataObject[i].GetComponent<ItemData>().itemDesc = weapon.description;
            ItemDataObject[i].GetComponent<Image>().sprite = weapon.weaponSprite;
            i++;
        }
    }


}
