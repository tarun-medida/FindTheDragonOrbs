using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class ObjectiveScript : MonoBehaviour
{

    public GameObject objectivePanel;
    public string[] popUps;
    public TMP_Text objectiveText;
    private int popUpIndex = 0;

    public checkIfPlayerHasPassedCollider checkIfPlayerHasPassed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ObjectivePopUps(popUpIndex);
        if (checkIfPlayerHasPassed.passed == true && GameManager.instance.enemyCounter == GameManager.instance.minionsToKillCount)
        {
            popUpIndex++;
            checkIfPlayerHasPassed.passed = false;
        }
        if(GameManager.instance.bossInstanceRef != null)
        {
            ObjectivePopUps(popUpIndex);
        }
    }

    private void ObjectivePopUps(int popUpIndex)
    {
        objectiveText.SetText(popUps[popUpIndex]);
    }
}
