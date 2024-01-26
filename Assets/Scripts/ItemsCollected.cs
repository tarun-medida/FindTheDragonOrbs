using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemsCollected : MonoBehaviour
{
    public TMP_Text weaponTitle,weaponDesc;
    public ItemData[] ItemDataObject;
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
        weaponTitle.text = item.title;
        weaponDesc.text = item.itemDesc;
    }
}
