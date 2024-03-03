using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SetData : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    private void Update()
    {
        SetCoinsText();
    }

    private void SetCoinsText()
    {
        text.SetText("Coins: " + GameInstance.instance.coinsCollected.ToString());
    }
}
