using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    //public WeaponDatabase weaponDatabase;
    private FileHandler fileHandler;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // game data object to save and load data
    private GameData gameData;
    private float weaponDamage;

    private void Start()
    {
        //fileHandler= new FileHandler("E:\\Abhay\\Game Design\\G2M_FTDO\\FindTheDragonOrbs", "SAVE.sv");
        Debug.Log(Application.persistentDataPath);
        fileHandler = new FileHandler(Application.persistentDataPath, "SAVE.sv");
        LoadGame();
    }

    public void UpdateCoinsCollected(int coins)
    {
        // existing count + new coins collected after a play session
        // as we have gameData intialized here, we shall update the values of game data
        gameData.coinsCollected = gameData.coinsCollected  +  coins;
    }

    public void UpdateCoinsCollectedAfterPurchase(int coins)
    {
        gameData.coinsCollected = coins;
    }

    public void UpdatePortionsUsed(int portionsLeft)
    {
        // updating the current portions equipped with the portions used in-game
        gameData.portionsEquipped = portionsLeft;
    }

    public void UpdateLevelsCompleted(int levelCount)
    {
        // as there is only one level as of now and to ensure that levels completed doesn't increase everytime we play the same level
        /* Not applicable anymore because we have 2 levels as of 19/05/2024
        if (gameData.levelsCompleted == 0)
        {
            gameData.levelsCompleted += levelCount;
        }
        */
        if (gameData.levelsCompleted == 0)
            gameData.levelsCompleted += levelCount;
        else if (gameData.levelsCompleted == 1)
            gameData.levelsCompleted += levelCount;
        else
            // only 2 levels
            gameData.levelsCompleted = gameData.levelsCompleted;

    }

    // should be called when player equips a weapon in inventory
    public void UpdateWeaponEquipped(string weaponTitle)
    {
       gameData.weaponEquipped = weaponTitle;
    }

    public void updateWeaponsPurchased(List<string> weaponsCollected)
    {
      gameData.weaponsCollected = weaponsCollected;
        
    }

    // new game data object with every data reset to default
    public void NewGame()
    {
        Debug.Log("Creating new save data...");
        gameData = new GameData();
    }

    // this will be called as soon as the player in-game presses exit and goes to main menu
    public void SaveGame()
    {
        // save game which will write contents to a file passing in updated game object
        // FileHandler.SaveGameData(this.gameData)
        Debug.Log("Saving game...");
        fileHandler.SaveData(gameData);

    }

    public void LoadGame()
    {
        // the save file will be read here and if there is no game data then new game else get the game data and update it to the variables in game manager
        GameData loadedGameData = fileHandler.LoadData();
        Debug.Log("Loading data...." + loadedGameData);
        // setting game data with loaded data from the save file
        gameData = loadedGameData;
        // if no save data exists, start from default state which is new game
        if (gameData == null)
        {
            Debug.Log("No Save Data Found. Loading with Default values...");
            NewGame();
        }
        else
        {
            Debug.Log("Save Data Found...");

        }
    }

    public GameData getGameData()
    {
        return gameData;
    }

    public void setWeaponDamage(float damage)
    {
        weaponDamage= damage;
    }

    public float getEquippedWeaponDamage()
    {
        return weaponDamage;
    }

}

/* 
Debug.Log("Coins:" + gameData.coinsCollected + " Portions:" + gameData.portionsEquipped + "Weapon:" + gameData.weaponEquipped);
foreach(var weapon in gameData.weaponsCollected)
{
Debug.Log("Weapon:" + weapon);
}
*/