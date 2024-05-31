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
    public SpriteRenderer sprite;
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
                    playerMovement.Invoke("Dead", 0.5f);
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

    private void FixedUpdate()
    {
        if (playerMovement.moveInput.x > 0)
        {
            playerMovement.spriteRenderer.flipX = false;
            playerMovement.gameObject.BroadcastMessage("FacingRight", true);
        }
        else if (playerMovement.moveInput.x < 0)
        {
            playerMovement.spriteRenderer.flipX = true;
            playerMovement.gameObject.BroadcastMessage("FacingRight", false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Defeated()
    {
        
        animator.SetTrigger("Defeated");
       
        if(gameObject.tag == "Minion")
        {
            no_of_coins = Random.Range(1, 4);
            for (int i = 0; i < no_of_coins; i++)
            {
                Instantiate(coins, transform.position, Quaternion.identity);
            }
            GameManager.instance.UpdateEnemyCounter();
            Destroy(gameObject);
        }
        if(gameObject.tag == "Dummy")
        {
            Destroy(gameObject);
        }
    }

    // Enemy's Hit Function/ Player getting hurt
    public void Hit(float damage)
    {
        Health = health - damage;
        animator.SetTrigger("hurt");
        StartCoroutine(FlashForDamage());
        AudioManager.instance.PlaySFX("PlayerHurtSFX");
        //PlayerHurtSound.Play();
        
    }

    // Player's Hit Function which adds force/ Enemies getting hurt
    public void Hit(float damage,Vector2 push)
    {
        animator.SetTrigger("hurt");
        StartCoroutine(FlashForDamage());
        //FloatingDamage
        Health = health - damage;
        rb.AddForce(push);
        healthBar.UpdateHealth(health,maxHealth);
        animator.SetTrigger("unhurt");
    }


    IEnumerator FlashForDamage()
    {
        sprite.color= new Color(0.1792453f, 0.1792453f, 0.1792453f, 1f);
        yield return new WaitForSeconds(0.2f);
        sprite.color = Color.white;
    }
}
