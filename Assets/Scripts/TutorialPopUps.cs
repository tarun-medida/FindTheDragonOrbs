using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TutorialPopUps : MonoBehaviour
{
    public GameObject[] popUps;
    private int popUpIndex;
    //variable used for blocking player from entering play area

    public bool enterPlayArea = false;
    private void Update()
    {
        for(int i = 0; i < popUps.Length-1; i++)
        {
            if (i == popUpIndex)
            {
                popUps[i].gameObject.SetActive(true);
            }
            else
            {
                popUps[i].gameObject.SetActive(false);
            }
        }
        if (popUpIndex == 0)
        {
            if(Input.GetKeyDown(KeyCode.W)||Input.GetKeyDown(KeyCode.S))
            {
                popUpIndex++;
                
            }
        }
        else if (popUpIndex == 1)
        {
            if(Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.D))
            {
                popUpIndex++;
                
            }
        }
        else if (popUpIndex==2)
        {
            if (Input.GetKey(KeyCode.K))
            {
                popUpIndex++;
                enterPlayArea = true;
            }
            
        }
        else if (popUpIndex == 3)
        {
            Destroy(popUps[popUpIndex]);
        }

    }
}
