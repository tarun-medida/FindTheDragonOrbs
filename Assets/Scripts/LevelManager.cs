using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] levels; 

    private void Start()
    {
        ShowUnlockedLevels();
    }

    public void LoadLevel(string Level)
    {
        if (SceneManager.GetSceneByName(Level) != null)
        {
            if (Level == "Level1")
                SceneManager.LoadScene(Level);
            else if (Level == "Level2" && GameInstance.instance.getGameData().levelsCompleted >= 1)
                SceneManager.LoadScene(Level);
            else if (Level == "Level3" && GameInstance.instance.getGameData().levelsCompleted >= 2)
                SceneManager.LoadScene(Level);
            else if (Level == "Level4" && GameInstance.instance.getGameData().levelsCompleted >= 3)
                SceneManager.LoadScene(Level);
            else if (Level == "Level5" && GameInstance.instance.getGameData().levelsCompleted >= 4)
                SceneManager.LoadScene(Level);
            else if (Level == "Level6" && GameInstance.instance.getGameData().levelsCompleted >= 5)
                SceneManager.LoadScene(Level);
            else if (Level == "Level7" && GameInstance.instance.getGameData().levelsCompleted >= 6)
                SceneManager.LoadScene(Level);
        }
        else
        {
            Debug.Log(Level + " Not Found!");
        }
    }
    private void ShowUnlockedLevels()
    {
        int i = 0;
        for(i = 0; i < GameInstance.instance.getGameData().levelsCompleted; i++)
        {
            levels[i].GetComponent<Image>().color = Color.white;
        }
        if(i < 2)
            levels[i].GetComponent<Image>().color = Color.white;
    }

}
