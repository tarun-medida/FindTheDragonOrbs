using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuyItemsWithCoins : MonoBehaviour
{
    public GameManager gameManager;

    public TMP_Text quantity;
    public TMP_Text price;
    public int totalPrice;
    public int qty;
    public int amount;

    public void CalculateTotalPrice()
    {
        qty = int.Parse(quantity.text);
        amount = int.Parse(price.text);

        totalPrice = qty * amount;
        gameManager.UpdateCoinsAfterPurchase(totalPrice,qty);

    }
}
