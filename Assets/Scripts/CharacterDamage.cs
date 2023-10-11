using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;
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
    public float Health
    {
        set
        {
            health = value;
            if (health <= 0)
            {
                if(gameObject.tag== "Player")
                {
                    Time.timeScale = 0;
                }
                Defeated();
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
        
    }
    public void Defeated()
    {
        
        animator.SetTrigger("Defeated");
        
        if (gameObject.tag != "Player")
        {
            Destroy(gameObject);
            no_of_coins = Random.Range(1, 4);
            for (int i = 0; i < no_of_coins; i++)
            {
                Instantiate(coins, transform.position, Quaternion.identity);
            }
        }
    }
    public void Hit(float damage)
    {
        Health = health - damage;
    }

    public void Hit(float damage,Vector2 push)
    {
        Health = health - damage;
        rb.AddForce(push);
        healthBar.UpdateHealth(health,maxHealth);
    }
}
