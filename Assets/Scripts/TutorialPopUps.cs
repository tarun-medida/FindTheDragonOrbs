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
    public GameObject Dummy;
    public GameObject cutScene;

    //variable used for blocking player from entering play area

    public bool enterPlayArea = false;
    private void Start()
    {
        if (GameInstance.instance.getGameData().tutorialCompleted == false)
        {   // play cut scene
            if (cutScene != null)
            {
                AudioManager.instance.musicSource.Pause();
                cutScene.SetActive(true);
                StartCoroutine(PlayCustScene());
            }
            tutorialPanel.SetActive(true);
        }
        else
        {
            popUpIndex = popUps.Length;
            Destroy(Dummy);
            enterPlayArea = true;
        }
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
            if (Input.GetKeyDown(KeyCode.LeftShift))
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
            if (Input.GetKey(KeyCode.J))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 5)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKey(KeyCode.L))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 6)
        {
            TutorialPopUp(popUpIndex);
            if (Input.GetKey(KeyCode.F))
            {
                popUpIndex++;
            }
        }
        else if (popUpIndex == 7)
        {       
            TutorialPopUp(popUpIndex);
            if (Input.GetKey(KeyCode.K))
            {
                Dummy.GetComponent<CircleCollider2D>().enabled = true;
                Dummy.GetComponent<BoxCollider2D>().enabled = true;
                popUpIndex++;
                enterPlayArea = true;
            }
        }
        else if (popUpIndex == popUps.Length)
        {
            //Destroy(tutorialPanel);
            GameInstance.instance.getGameData().tutorialCompleted = true;
            GameInstance.instance.SaveGame();
        }
    }

    private void TutorialPopUp(int popUpIndex)
    {
        tutorialText.SetText(popUps[popUpIndex]);
    }

    IEnumerator PlayCustScene()
    {
        float delay = 2f;
        float animTime = cutScene.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        float timeToDestroy = animTime + delay;
        Destroy(cutScene.gameObject, timeToDestroy);
        yield return new WaitForSeconds(timeToDestroy);
        AudioManager.instance.musicSource.UnPause();
    }
}
