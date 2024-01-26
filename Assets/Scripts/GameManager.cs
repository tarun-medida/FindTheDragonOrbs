using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;



// Keep track of player attributes and stats
// In game
/* Player hearts
 * health portions purchased
 * item equipped
 * special attack info of the item equipped
 * enemy counter
 * Enemy spawn limiter per level that will be taken by enemy spanwer script
 * Game win and lose condition
 * Coins collected
 // Main menu
 * Items purchased
 * updating player stats and attributes on purchase
 * Coins held
*/


public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject deadMenuUI;
    public AudioSource LevelbackgroundScore;
    int noOfHealthPortions;
    int coinsCollected;
    string equippedItemTitle;
    string equippedItemDescription;
    public TMP_Text equippedItemTitleText, equippedItemDescriptionText, equippedPortionsCountText, portionsLimitCountText;
    Rigidbody2D rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //LevelbackgroundScore.Play();
        noOfHealthPortions = 5;
        coinsCollected = 40;
        equippedItemTitle = "DrogFire";
        equippedItemDescription = "DMG: 10\r\nSpecial: Fire DMG\r\n\r\nCreated from the scales of the atlantic dragon DrogBorne";
        UpdateEquppedItemContentInInventory();
        UpdateEquippedPortionsCount();
    }
    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void OnClickTryAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        Application.Quit();
    }

    public void OnResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void UpdateEquppedItemContentInInventory()
    {
        equippedItemTitleText.text = equippedItemTitle;
        equippedItemDescriptionText.text = equippedItemDescription;
    }

    public void UpdateEquippedPortionsCount()
    {
        equippedPortionsCountText.text = noOfHealthPortions.ToString();
        portionsLimitCountText.text = 30.ToString();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
