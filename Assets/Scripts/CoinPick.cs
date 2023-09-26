using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPick : MonoBehaviour
{
    public enum PickupObject { COIN,GEM};

    public PickupObject type = PickupObject.COIN;

    private static int no_of_coins = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (type == PickupObject.COIN)
            {
                no_of_coins++;
                Debug.Log(no_of_coins);
                Destroy(gameObject);
            }
        }
    }

    internal void RandomCoins()
    {
        Debug.Log("Coinnsss");
    }
}
