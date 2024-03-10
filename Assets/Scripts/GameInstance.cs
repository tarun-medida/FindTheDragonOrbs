using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class GameInstance : MonoBehaviour
{
    public static GameInstance instance;
    public WeaponDatabase weaponDatabase;
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


    private void Start()
    {
        fileHandler= new FileHandler("E:\\Abhay\\Game Design\\G2M_FTDO\\FindTheDragonOrbs", "SAVE.sv");
        LoadGame();
    }

    public void updateCoinsCollected(int coins)
    {
        // existing count + new coins collected after a play session
        // as we have gameData intialized here, we shall update the values of game data
        //coinsCollected+= coins;
        
        this.gameData.coinsCollected = this.gameData.coinsCollected  +  coins;
        Debug.Log("Coins : " + this.gameData.coinsCollected.ToString());
    }

    public void updatePortionsUsed(int portionsLeft)
    {
        // updating the current portions equipped with the portions used in-game
        this.gameData.portionsEquipped = portionsLeft;
    }

    public void updateLevelsCompleted(int levelCount)
    {
        // as there is only one level as of now and to ensure that levels completed doesn't increase everytime we play the same level
        if (this.gameData.levelsCompleted == 0)
        {
            this.gameData.levelsCompleted += levelCount;
        }
    }

    public void GetWeaponsCollected()
    {
        weaponDatabase.ShowWeaponsCollected();
    }

    // new game data object with every data reset to default
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    // this will be called as soon as the player in-game presses exit and goes to main menu
    public void SaveGame()
    {
        // save game which will write contents to a file passing in updated game object
        // FileHandler.SaveGameData(this.gameData)
        fileHandler.SaveData(this.gameData);

    }

    public void LoadGame()
    {
        // the save file will be read here and if there is no game data then new game else get the game data and update it to the variables in game manager
        GameData loadedGameData = fileHandler.LoadData();
        Debug.Log("Loading data...." + loadedGameData);
        // setting game data with loaded data from the save file
        this.gameData = loadedGameData;
        // if no save data exists, start from default state which is new game
        if (this.gameData == null)
        {
            Debug.Log("No Save Data Found. Loading with Default values...");
            NewGame();
        }
        else
        {
            Debug.Log("Save Data Found...");
            Debug.Log("Coins:" + gameData.coinsCollected + " Portions:" + gameData.portionsEquipped);
        }
    }

    public GameData getGameData()
    {
        return this.gameData;
    }

}
