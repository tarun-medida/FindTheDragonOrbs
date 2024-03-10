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
        //text.SetText("Coins: " + GameInstance.instance.getGameData().coinsCollected.ToString());
        // coins collected will be updated by loaded data in start() of Gamemanager and will fetch the value from there!
        text.SetText("Coins: " + GameManager.instance.getCoinsCollected().ToString());
    }
}
