using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NumberController : MonoBehaviour
{
    public TMP_Text numberText;
    private int number = 0;

    void Start()
    {
        UpdateNumberText();
    }

    public void IncreaseNumber()
    {
        number++;
        UpdateNumberText();
    }

    public void DecreaseNumber()
    {
        if(number >0)
        number--;

        UpdateNumberText();
    }

    private void UpdateNumberText()
    {
        numberText.text = number.ToString();
    }

}
