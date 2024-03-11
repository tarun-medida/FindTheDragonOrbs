using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsCollected : MonoBehaviour
{
    public ItemData[] ItemDataObject;
    public WeaponDatabase weaponDatabase;
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ViewItemDescription(ItemData item)
    {
        
        weaponDatabase.FindWeaponByTitle(item.title);
    }


}
