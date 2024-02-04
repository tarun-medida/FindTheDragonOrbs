using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;



// Keep track of player attributes and stats
// In game
/* Player hearts
 * health portions purchased
 * item equipped - done! not tested
 * special attack info of the item equipped - pending
 * enemy counter - done!
 * Game win and lose condition - done!
 * Coins collected - done!
 * Level Music
 // Main menu
 * Items purchased
 * updating player stats and attributes on purchase - pending
 * Coins held in the current scene - done!
 * Transfering state of level completion and coins between scenes - pending
*/


public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    // In Game Event UIs
    public GameObject pauseMenuUI, deadMenuUI, winMenuUI;
    // In Game Level Music
    public AudioSource LevelbackgroundScore;
    // In Game UI Elements
    int noOfHealthPortions;
    int coinsCollected;
    string equippedItemTitle;
    string equippedItemDescription;
    public TMP_Text equippedItemTitleText, equippedItemDescriptionText, equippedPortionsCountText, portionsLimitCountText;
    //portion drink variables
    private int heartsTracker;
    public GameObject HealthRegenObject;
    public TMP_Text healthRegenText;
    private Animator healtRegenAnimator;
    //Rigidbody2D rb;
    //In Game Characters to access
    public PlayerMovement player;
    public GameObject boss;
    private GameObject bossInstanceRef;
    // Level Parameters
    public int numberOfMinionsToSpawn;
    public int minionsToKillCount;
    public Transform[] randomBossSpawnLocations;
    private int enemyCounter = 0;
    private bool canSpawnBoss = false;


    public void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        //LevelbackgroundScore.Play();

        // *** PLAYER HEALTH INITIALIZATION ***
        // setting player number of hearts at start
        noOfHealthPortions = 5;
        // at start what was the maximum number of hearts the player started with
        // later on when player purchases new health in inventory then the no_of_hearts will be changed
        heartsTracker = player.no_of_hearts;
        // setting the In Game UI Elements.
        healthRegenText.text = noOfHealthPortions.ToString();
        healtRegenAnimator = HealthRegenObject.GetComponent<Animator>();
        //*****************************************
        coinsCollected = 40;
        equippedItemTitle = "DrogFire";
        equippedItemDescription = "DMG: 10\r\nSpecial: Fire DMG\r\n\r\nCreated from the scales of the atlantic dragon DrogBorne";
        //UpdateEquppedItemContentInInventory();
        //UpdateEquippedPortionsCount();

    }

    void Update()
    {
        // *** BOSS SPAWN CONDITION ***
        if (enemyCounter == minionsToKillCount)
        {
            Debug.Log("Spawn");
                int randSpawnPoint = UnityEngine.Random.Range(0, randomBossSpawnLocations.Length);
                // as there going to be only one boss, getting the instantiated boss game object for reference
                bossInstanceRef = Instantiate(boss, randomBossSpawnLocations[randSpawnPoint].position, Quaternion.identity);
                // no a cool thing to do, but won't make that much difference, this to ensure that only one instance of the boss will be spawned
                enemyCounter++;
        }

        
        // *** PAUSE GAME ***
        // activate Pause Menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
        // *** LOSE CONDITION ***
        // checking player health to activate Game Over Menu
        if (player.health <= 0)
        {
            deadMenuUI.SetActive(true);
        }

        // *** WIN CONDITION ***
        // checking boss health and object to activate Game Win Screen
        if (bossInstanceRef != null)
        {
            if (bossInstanceRef.GetComponent<CharacterDamage>().Health <= 0)
            {
                winMenuUI.SetActive(true);
                Destroy(bossInstanceRef);
                // testing!! upon win the walk sound kept playing and when scene loaded the game was bugged
                Time.timeScale = 0;
            }
        }
        // *** UPDATE HEALTH PORTION UI if all portions are consumed ***
        if (noOfHealthPortions <= 0)
        {
            healtRegenAnimator.SetBool("NoPortions", true);
        }

    }

    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
    }
    
    /* Unused, only 1 level as of 28_01_2024
    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
    */

    public void OnClickTryAgain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        
    }
    public void OnResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        Application.Quit();
    }

    // Update Player's Current Weapon
    public void UpdateEquppedItemContentInInventory()
    {
        equippedItemTitleText.text = equippedItemTitle;
        equippedItemDescriptionText.text = equippedItemDescription;
    }

    // Update Player's Portions
    public void UpdateEquippedPortionsCount()
    {
        equippedPortionsCountText.text = noOfHealthPortions.ToString();
        portionsLimitCountText.text = 30.ToString();
    }

    // Update Player's Health when drinking portion
    public void drinkPortion()
    {
        // *** DRINK PORTION ***
        if (noOfHealthPortions > 0)
        {
            healtRegenAnimator.SetBool("NoPortions", false);
            // decrease the no of health portions
            if (getNoOfHeartsFromHealth(player.health) < heartsTracker)
            {
                healtRegenAnimator.SetTrigger("Regen");
                noOfHealthPortions -= 1;
                healthRegenText.text = noOfHealthPortions.ToString();
                player.UpdateHealth();
            }
            else
            {
                Debug.Log("Already full health");
            }
        }
        else
        {
            Debug.Log("No More Portions");
        }
    }
    private int getNoOfHeartsFromHealth(float hearts)
    {
        hearts = (int)(player.health * player.no_of_hearts) / player.maxHealth;
        // using ceil to ensure that portion can only be used if one heart is totally gone.
        return (int)Mathf.Ceil(hearts);
    }
    // *******************************************************

    //*** ENEMIES KILLED COUNTER ***
    public void UpdateEnemyCounter()
    {
        enemyCounter++;
    }

    // *** TO GET ENEMIES KILLED COUNT FOR SHOWING IN UI ***
    public int GetEnemiesKilledCount()
    {
        return enemyCounter;
    }

    // *** KEEPING TRACK OF THE COINS COLLECTED SO FAR ***
    public void UpdateCoinsCollected()
    {
        coinsCollected++;
    }
}
