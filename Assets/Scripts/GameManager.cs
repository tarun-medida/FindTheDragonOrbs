using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;
using static WeaponDatabase;
using Image = UnityEngine.UI.Image;



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
    int coinsCollected = 0;
    string equippedItemTitle;
    string equippedItemDescription;
    public TMP_Text equippedItemTitleText, equippedItemDescriptionText, equippedPortionsCountText, portionsLimitCountText, coinsCollectedInventoryText,coinsCollectedWeaponsStoreText, coinsCollectedPortionsStoreText;
    public Image equippedWeaponSprite;
    //portion drink variables
    private int heartsTracker;
    public GameObject HealthRegenObject;
    public TMP_Text healthRegenText;
    private Animator healtRegenAnimator;
    //Rigidbody2D rb;
    //In Game Characters to access
    public PlayerMovement player;
    public GameObject boss;
    public GameObject bossInstanceRef;
    // Level Parameters
    public int numberOfMinionsToSpawn;
    public int minionsToKillCount;
    public Transform[] randomBossSpawnLocations;
    public int enemyCounter = 0;
    private GameData loadedGameData;
    public WeaponDatabase weaponDatabase;
    public ItemsCollected itemsCollected;

    public void Start()
    {
        //rb = GetComponent<Rigidbody2D>();
        LevelbackgroundScore.Play();
        // Loading Game Data
        loadedGameData = GameInstance.instance.getGameData();
        // *** PLAYER HEALTH INITIALIZATION ***
        noOfHealthPortions = loadedGameData.portionsEquipped;
        // at start what was the maximum number of hearts the player started with
        // later on when player purchases new health in inventory then the no_of_hearts will be changed
        if (player != null)
        {
            heartsTracker = player.no_of_hearts;
            // setting the In Game UI Elements.
            healthRegenText.text = noOfHealthPortions.ToString();
            healtRegenAnimator = HealthRegenObject.GetComponent<Animator>();
        }
        //*****************************************
        // when levels starts the coinsCollected should be zero and newly updated value has to be updated, so it will be zero in-game
        if(player == null)
            coinsCollected = loadedGameData.coinsCollected;
        // Loading the weapon sprites from resources folder
        if (weaponDatabase != null)
        {
            weaponDatabase.LoadWeaponSprites();
        }
        UpdateEquippedItemContentInInventory(loadedGameData.weaponEquipped);
        UpdateEquippedPortionsCount();
        // Loading the collected weapons from save file and loading in inventory
        LoadCollectedWeaponsAndUpdateAvailableWeaponsForPurchase(loadedGameData);
    }


    private void FixedUpdate()
    {
        if(player == null)
        {
            SetCoinsTextAcrossMainMenu();
        }
        
        
    }

    void Update()
    {
        // *** BOSS SPAWN CONDITION ***
        if (enemyCounter == minionsToKillCount && player != null)
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
        // update game data as well,such as coins collected, portions left
        if (player != null && player.health <= 0)
        {
            deadMenuUI.SetActive(true);
            // to stop player movement
            player.GetComponent<PlayerInput>().enabled = false;
            // to ensure enemies stop attack on player death
            player.GetComponent<BoxCollider2D>().enabled = false;
            // saving the game upon lose which will update game data, because you will have collected some coins and used some portions
        }

        // *** WIN CONDITION ***
        // checking boss health and object to activate Game Win Screen
        // update game data as well, such as coins collected, portions left
        if (bossInstanceRef != null)
        {
            if (bossInstanceRef.GetComponent<CharacterDamage>().Health <= 0)
            {
                winMenuUI.SetActive(true);
                Destroy(bossInstanceRef);
                // testing!! upon win the walk sound kept playing and when scene loaded the game was bugged
                // saving the game upon win which will update game data
                Time.timeScale = 0;
            }
        }
        // *** UPDATE HEALTH PORTION UI if all portions are consumed ***
        if (player != null && noOfHealthPortions <= 0)
        {
            healtRegenAnimator.SetBool("NoPortions", true);
        }

    }

    private void SetCoinsTextAcrossMainMenu()
    {
        coinsCollectedInventoryText.SetText("Atheleons: " + GetCoinsCollected().ToString());
        coinsCollectedWeaponsStoreText.SetText("Atheleons: " + GetCoinsCollected().ToString());
        coinsCollectedPortionsStoreText.SetText("Atheleons: " + GetCoinsCollected().ToString());
    }

    private void UpdateGameDataOnWin()
    {
        GameInstance.instance.UpdateCoinsCollected(coinsCollected);
        GameInstance.instance.UpdatePortionsUsed(noOfHealthPortions);
        GameInstance.instance.UpdateLevelsCompleted(1);
    }

    private void UpdateGameDataOnLose()
    {
        GameInstance.instance.UpdateCoinsCollected(coinsCollected);
        GameInstance.instance.UpdatePortionsUsed(noOfHealthPortions);

    }

    private void UpdateCoinsAndPortionsAfterPurchase()
    {
        GameInstance.instance.UpdateCoinsCollectedAfterPurchase(coinsCollected);
        GameInstance.instance.UpdatePortionsUsed(noOfHealthPortions);
    }

    public void UpdateCoinsAndCollectedWeaponsAfterPurchase()
    {
        GameInstance.instance.UpdateCoinsCollectedAfterPurchase(coinsCollected);
        List<string> updatedCollectedWeapons = new();
        foreach(var weapons in weaponDatabase.collectedWeapons)
        {
            updatedCollectedWeapons.Add(weapons.title);
        }
        GameInstance.instance.updateWeaponsPurchased(updatedCollectedWeapons);
        
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
        player.walkSound.Pause();
        
    }
    public void OnResume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1;
    }

    public void MainMenu()
    {
        if(winMenuUI.activeInHierarchy)
        {
            UpdateGameDataOnWin();
            GameInstance.instance.SaveGame();
        }
        else if(deadMenuUI.activeInHierarchy)
        {
            UpdateGameDataOnLose();
            GameInstance.instance.SaveGame();
        }
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void OnExit()
    {
        GameInstance.instance.SaveGame();
        UnityEngine.Application.Quit();
    }

    // Update Player's Current Weapon
    public void UpdateEquippedItemContentInInventory(string loadedWeaponTitle)
    {
        if (weaponDatabase != null)
        {
            Weapons weapon = weaponDatabase.GetWeaponDetailsByTitle(loadedWeaponTitle);
            
            if (weapon != null)
            {
                equippedItemTitle = weapon.title;
                equippedItemDescription = weapon.description;
                equippedItemTitleText.text = equippedItemTitle;
                equippedItemDescriptionText.text = equippedItemDescription;
                equippedWeaponSprite.sprite = weapon.weaponSprite;
            }
            else
            {
                Debug.Log("Weapon not Found!");
            }
        }
        
    }

    // Update Player's Portions in inventory
    public void UpdateEquippedPortionsCount()
    {
        if (equippedPortionsCountText != null)
        {
            equippedPortionsCountText.text = noOfHealthPortions.ToString();
            portionsLimitCountText.text = 30.ToString();
        }
    }

    // Update Player's Health when drinking portion
    public void DrinkPortion()
    {
        // *** DRINK PORTION ***
        if (noOfHealthPortions > 0)
        {
            healtRegenAnimator.SetBool("NoPortions", false);
            // decrease the no of health portions
            if (GetNoOfHeartsFromHealth(player.health) < heartsTracker)
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


    // Load Collected Weapons and update weaponDatabase
    public void LoadCollectedWeaponsAndUpdateAvailableWeaponsForPurchase(GameData loadedGameData)
    {
        if(player == null)
        {
            foreach (var weaponTitle in loadedGameData.weaponsCollected)
            {
                Weapons weapon = weaponDatabase.GetWeaponDetailsByTitle(weaponTitle);
                weaponDatabase.collectedWeapons.Add(weapon);
            }

        }
        

    }


    //Equip Item From Inventory
    public void EquipItem()
    {
        Debug.Log(itemsCollected.weaponTitle.text.ToString());
        Weapons weapon = weaponDatabase.GetWeaponDetailsByTitle(itemsCollected.weaponTitle.text.ToString());
        // if weapon exists and if the current weapon is not the same as the currenlty equipped weapon
        if (weapon != null && equippedItemTitle != weapon.title)
        {
            equippedItemTitle = weapon.title;
            equippedItemDescription = weapon.description;
            equippedItemTitleText.text = equippedItemTitle;
            equippedItemDescriptionText.text = equippedItemDescription;
            equippedWeaponSprite.sprite = weapon.weaponSprite;
            GameInstance.instance.UpdateWeaponEquipped(equippedItemTitle);
            GameInstance.instance.SaveGame();
        }
        else
        {
            Debug.Log("Weapon not Found or Weapon Already Equipped");
        }
    }


    private int GetNoOfHeartsFromHealth(float hearts)
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
        // this is updating the coins collected when in-game which will start from zero during every level
        coinsCollected++;
    }

    public int GetCoinsCollected()
    {
        return coinsCollected;
    }

    public void UpdateCoinsAfterPurchasePortion(int totalPrice,int noOfPortions)
    {
        if (coinsCollected >= totalPrice)
        {
            coinsCollected -= totalPrice;
            Debug.Log("You have " + coinsCollected + " coins");
            noOfHealthPortions += noOfPortions;
            Debug.Log("Now you have " + noOfHealthPortions + " portions");
            UpdateCoinsAndPortionsAfterPurchase();
            UpdateEquippedPortionsCount();
            GameInstance.instance.SaveGame();
        }
        else if (coinsCollected < totalPrice) 
        {
            Debug.Log("You don't have enough coins.");
        }
    }
    public bool UpdateCoinsAfterPurchaseWeapon(int totalPrice)
    {
        if (coinsCollected >= totalPrice)
        {
            coinsCollected -= totalPrice;
            Debug.Log(totalPrice);
            Debug.Log("Congratulations you bought the weapon");
            return true;
        }
        else if (coinsCollected < totalPrice)
        {
            Debug.Log("You don't have enough coins.");
            return false;
        }
        return false;
    }

}
