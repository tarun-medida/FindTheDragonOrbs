using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public void LoadLevel(string Level)
    {
        if (SceneManager.GetSceneByName(Level) != null)
        {
            SceneManager.LoadScene(Level);
        }
        else
        {
            Debug.Log(Level + " Not Found!");
        }
    }
}
