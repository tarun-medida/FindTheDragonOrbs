using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Keep track of player attributes and stats
// In game
/* Player hearts
 
- health portions purchased
- item equipped
- special attack info of the item equipped
- enemy counter
- Enemy spawn limiter per level that will be taken by enemy spanwer script
- Game win and lose condition
- Coins collected
// Main menu
- Items purchased
- updating player stats and attributes on purchase
- Coins held
*/
public class GameManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject deadMenuUI;
    public AudioSource LevelbackgroundScore;
    Rigidbody2D rb;
    public void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //LevelbackgroundScore.Play();
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

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            pauseMenuUI.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
