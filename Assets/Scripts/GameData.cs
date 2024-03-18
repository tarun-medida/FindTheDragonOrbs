using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int coinsCollected;
    public int levelsCompleted;
    public List<string> weaponsCollected;
    public int portionsEquipped;
    public string weaponEquipped;

    public GameData()
    {
        coinsCollected = 0;
        levelsCompleted = 0;
        // DrogFire is the default weapon
        weaponsCollected = new List<string>
        {
            "Sword Of Helios"
        };
        portionsEquipped = 0;
        weaponEquipped = "Sword Of Helios";
    }
}
