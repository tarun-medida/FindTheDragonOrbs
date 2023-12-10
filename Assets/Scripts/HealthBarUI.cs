using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    // Start is called before the first frame update
    // Player's current health
    public float health;
    // Player's max health
    public float maxHealth;
    // Sprite of the heart
    public Sprite Heart;
    public Sprite EmptyHeart;
    public Image[] hearts;
    private int maxHealthHearts;
    public PlayerMovement player;
    void Start()
    {
        // intialize on start
        maxHealth = player.maxHealth;
        health = (int)(player.health * player.no_of_hearts) / maxHealth;
        maxHealthHearts = player.no_of_hearts;
    }

    // Update is called once per frame
    void Update()
    {
        // update every frame, so when player loses health, this function will take the current health and decide how many hearts to display
        update_health();
    }

    private void update_health()
    {
        maxHealth = player.maxHealth;
        health = (int)(player.health * player.no_of_hearts) / maxHealth;
        maxHealthHearts = player.no_of_hearts;
        for (int i = 0; i < hearts.Length; i++)
        {
            // mention what sprite image has to be added based on health
            if (i < health)
            {
                hearts[i].sprite = Heart;
            }
            else
            {
                hearts[i].sprite = EmptyHeart;
            }
            // based on number of hearts the player has during the level
            // like the player has a limit of 3 hearts before level up then only 3 heart slots have to be drawn and the remaing will not be enabled/drawn
            // when the player levels up his max no of hearts is 5
            // then 5 heart sprites will be drawn in total.
            // check in editor where the image will be disabled when greater than maxHealthHearts
            if (i < maxHealthHearts)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }

    }
}