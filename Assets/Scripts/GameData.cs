using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public class GameData
{
    public int coinsCollected;
    public int levelsCompleted;
    public string[] weaponsBought;
    public int portionsEquipped;
    public string weaponEquipped;

    public GameData()
    {
        this.coinsCollected = 0;
        this.levelsCompleted = 0;
        this.weaponsBought = null;
        this.portionsEquipped = 0;
        this.weaponEquipped= "DrogFire";
    }
}
