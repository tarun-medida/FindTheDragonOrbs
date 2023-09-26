using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public void OnClickPlay()
    {
        SceneManager.LoadScene(1);
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
