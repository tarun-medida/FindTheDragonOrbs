using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    public WeaponDatabase weaponDatabase;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public int coinsCollected = 0;


    public void updateCoinsCollected(int coins)
    {
        coinsCollected = coins;
    }
    public void GetWeaponsCollected()
    {
        weaponDatabase.ShowWeaponsCollected();
    }

}
