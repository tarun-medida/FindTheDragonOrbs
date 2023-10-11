using TMPro;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static Score instance;
    public TMP_Text scoreText;
    private int no_of_coins = 0;

    private void Awake()
    {
        instance = this; 
    }

    // Update is called once per frame
    void Start()
    {
        scoreText.text = no_of_coins.ToString();
    }
    public void IncreaseScore()
    {
        no_of_coins++;
        scoreText.text = no_of_coins.ToString();
    }
}
