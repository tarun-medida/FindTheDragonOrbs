using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoinPick : MonoBehaviour
{
    public enum PickupObject { COIN,GEM};

    public PickupObject type = PickupObject.COIN;
    private static int coins = 0;
    public int score=0;
    public AudioClip coinPickup;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            
            if (type == PickupObject.COIN)
            {
                coins++;
                score = coins;
                //Debug.Log(score);
                AudioSource.PlayClipAtPoint(coinPickup,transform.position,1f);
                Destroy(gameObject);
                Score.instance.IncreaseScore();
            }
        }
    }
}
