using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    // Start is called before the first frame update

    public Animator pageFlipAnimator;
    private bool mainMenuFlag = false, inventoryFlag = false, shopFlag = false, weaponsFlag = false, portionsFlag = false;
    public GameObject mainMenuPage, inventoryPage, shopPage, startScreen, weaponsPage, potionsPage, startText, bookNameText;
    //public AudioSource pageFlip;
    void Start()
    {
    }

    public void OpenBook()
    {
        // hiding the start text to open book
        bookNameText.SetActive(false);
        startText.SetActive(false);
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
        else if(weaponsFlag == true)
        {
            shopPage.SetActive(false);
            weaponsPage.SetActive(true);
        }
        else if(portionsFlag == true)
        {
            shopPage.SetActive(false);
            potionsPage.SetActive(true);
        }
    }


    public void ShowMainPage()
    {
        if (mainMenuPage.activeSelf == false)
        {
            pageFlipAnimator.SetTrigger("Select");
            AudioManager.instance.PlaySFX("PageFlipSFX");
            mainMenuFlag = true;
            inventoryFlag = false;
            shopFlag = false;
            weaponsFlag = false;
            portionsFlag = false;
            inventoryPage.SetActive(false);
            shopPage.SetActive(false);
            weaponsPage.SetActive(false);
            potionsPage.SetActive(false);
            StartCoroutine(Delay(1.5f, true));
        }
    }

    public void ShowInventoryPage()
    {
        if (inventoryPage.activeSelf == false)
        {
            pageFlipAnimator.SetTrigger("Select");
            AudioManager.instance.PlaySFX("PageFlipSFX");
            inventoryFlag = true;
            mainMenuFlag = false;
            shopFlag = false;
            weaponsFlag = false;
            portionsFlag = false;
            mainMenuPage.SetActive(false);
            shopPage.SetActive(false);
            weaponsPage.SetActive(false);
            potionsPage.SetActive(false);
            StartCoroutine(Delay(1.5f, true));
        }
        

    }

    public void ShowShopPage()
    {
        if (shopPage.activeSelf == false)
        {
            pageFlipAnimator.SetTrigger("Select");
            AudioManager.instance.PlaySFX("PageFlipSFX");
            shopFlag = true;
            mainMenuFlag = false;
            inventoryFlag = false;
            weaponsFlag = false;
            portionsFlag = false;
            mainMenuPage.SetActive(false);
            inventoryPage.SetActive(false);
            weaponsPage.SetActive(false);
            potionsPage.SetActive(false);
            StartCoroutine(Delay(1.5f, true));
        }
    }

    public void ShowWeaponsPage()
    {
        if (weaponsPage.activeSelf == false)
        {
            pageFlipAnimator.SetTrigger("Select");
            AudioManager.instance.PlaySFX("PageFlipSFX");
            weaponsFlag = true;
            shopFlag = false;
            mainMenuFlag = false;
            inventoryFlag = false;
            mainMenuPage.SetActive(false);
            inventoryPage.SetActive(false);
            potionsPage.SetActive(false);
            StartCoroutine(Delay(1.5f, true));
        }
    }

    public void ShowPortionsPage()
    {
        if (potionsPage.activeSelf == false)
        {
            pageFlipAnimator.SetTrigger("Select");
            AudioManager.instance.PlaySFX("PageFlipSFX");
            portionsFlag = true;
            shopFlag = false;
            weaponsFlag = false;
            mainMenuFlag = false;
            inventoryFlag = false;
            mainMenuPage.SetActive(false);
            inventoryPage.SetActive(false);
            weaponsPage.SetActive(false);
            StartCoroutine(Delay(1.5f, true));
        }
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
            // to open the book and show the main menu rather than plain background!
            mainMenuFlag= true;
            ShowPage();
            ShowMainPage();
        }
            
    }
}
