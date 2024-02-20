using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TutorialPopUps : MonoBehaviour
{
    public GameObject tutorialPanel;
    public string[] popUps;
    public TMP_Text tutorialText;
    private int popUpIndex=0;
    public float wordSpeed;
    //variable used for blocking player from entering play area

    public bool enterPlayArea = false;
    private void Start()
    {
        tutorialPanel.SetActive(true);
    }
    private void Update()
    {
        if (popUpIndex == 0)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 1)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
            }
        }
        else if(popUpIndex == 2)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex==3)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKey(KeyCode.K))
            {
                popUpIndex++;
            }          
        }
        else if (popUpIndex == 4)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKey(KeyCode.F))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKey(KeyCode.K))
            {
                popUpIndex++;
                enterPlayArea = true;
            }
        }
        else if (popUpIndex == popUps.Length)
        {
            Destroy(tutorialPanel);
        }
    }

    private void TutorialPopUp(int popUpIndex)
    {
        tutorialText.SetText(popUps[popUpIndex]);
    }
}
