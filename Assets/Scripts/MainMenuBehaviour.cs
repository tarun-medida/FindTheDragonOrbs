using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator pageFlipAnimator;
    private bool mainMenuFlag = false,inventoryFlag = false,shopFlag = false;
    public GameObject mainMenuPage, inventoryPage, shopPage, startScreen;
    public AudioSource pageFlip;
    void Start()
    {
    }

    public void OpenBook()
    {
        pageFlipAnimator.SetTrigger("Open");
        StartCoroutine(Delay(1.35f,false));

    }


    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Console.WriteLine("Pressed");
            if(startScreen!= null)
            {
                OpenBook();
            }
        }
    }


    public void ShowPage()
    {
        if(mainMenuFlag == true)
        {
            mainMenuPage.SetActive(true);

        }
        else if(inventoryFlag == true)
        {
            inventoryPage.SetActive(true);

        }
        else if(shopFlag == true)
        {
            shopPage.SetActive(true);
            
        }
    }


    public void ShowMainPage()
    {
        pageFlipAnimator.SetTrigger("Select");
        pageFlip.Play();
        mainMenuFlag = true;
        inventoryFlag = false;
        shopFlag = false;
        inventoryPage.SetActive(false);
        shopPage.SetActive(false);
        StartCoroutine(Delay(1.5f, true));
    }

    public void ShowInventoryPage()
    {
        pageFlipAnimator.SetTrigger("Select");
        pageFlip.Play();
        inventoryFlag = true;
        mainMenuFlag = false;
        shopFlag= false;
        mainMenuPage.SetActive(false);
        shopPage.SetActive(false);
        StartCoroutine(Delay(1.5f, true));
        

    }

    public void ShowShopPage()
    {
        pageFlipAnimator.SetTrigger("Select");
        pageFlip.Play();
        shopFlag = true;
        mainMenuFlag = false;
        inventoryFlag= false;
        mainMenuPage.SetActive(false);
        inventoryPage.SetActive(false);
        StartCoroutine(Delay(1.5f, true));
    }


    IEnumerator Delay(float time, bool flag)
    {
        //yield on a new YieldInstruction that waits for 1.5 seconds.
        yield return new WaitForSeconds(time);
        if(flag == true)
            ShowPage();
        else
        {
            this.gameObject.SetActive(false);
            startScreen.SetActive(true);
        }
            
    }
}
