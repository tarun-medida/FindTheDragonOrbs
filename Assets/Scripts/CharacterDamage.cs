using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CharacterDamage : MonoBehaviour, IDamageable
{
    public float health;
    public float maxHealth;
    Rigidbody2D rb;
    Animator animator;
    public GameObject coins;
    private int no_of_coins;
    BossHealthBar healthBar;
    public PlayerMovement playerMovement;
    public bool winScreen = false;
    public AudioSource PlayerHurtSound;
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                Defeated();
                if (gameObject.tag == "Player")
                {
                    playerMovement.Invoke("Dead", 2f);
                }
            }
        }
        get
        {
            return health;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();   
        animator = GetComponent<Animator>();   
        healthBar = GetComponentInChildren<BossHealthBar>();
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(gameObject.tag == "Boss")
        {
            Debug.Log(health);
        }
        */
    }
    public void Defeated()
    {
        
        animator.SetTrigger("Defeated");
        
        if (gameObject.tag != "Player")
        {
            
            no_of_coins = Random.Range(1, 4);
            for (int i = 0; i < no_of_coins; i++)
            {
                Instantiate(coins, transform.position, Quaternion.identity);
            }
            if(gameObject.tag == "Minion")
            {
                Destroy(gameObject);
            }
        }
        else
        {
            // to stop player movement
            playerMovement.GetComponent<PlayerInput>().enabled = false;
            // to ensure enemies stop attack on player death
            playerMovement.GetComponent<BoxCollider2D>().enabled = false;
        }
    }
    public void Hit(float damage)
    {
        Health = health - damage;
        PlayerHurtSound.Play();
    }

    public void Hit(float damage,Vector2 push)
    {
        Health = health - damage;
        rb.AddForce(push);
        healthBar.UpdateHealth(health,maxHealth);
    }
}
