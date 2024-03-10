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

    public void CalculatePortionTotalPrice()
    {
        qty = int.Parse(quantity.text);
        amount = int.Parse(price.text);

        totalPrice = qty * amount;
        gameManager.UpdateCoinsAfterPurchasePortion(totalPrice,qty);
    }
    public void CalculateWeaponTotalPrice()
    {
        amount = int.Parse(price.text);

        totalPrice = qty * amount;
        gameManager.UpdateCoinsAfterPurchaseWeapon(totalPrice);
    }
}
