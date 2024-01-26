using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemData : MonoBehaviour
{
    public string title, itemDesc;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }


    void LoadData()
    {

    }

    void StoreDataInInventory(string purchasedItemTitle,string purchasedItemDesc)
    {
        title= purchasedItemTitle;
        itemDesc= purchasedItemDesc;
    }
}
