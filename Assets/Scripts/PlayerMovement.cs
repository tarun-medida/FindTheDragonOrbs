using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static CoinPick;

public class PlayerMovement : MonoBehaviour 
{
    CharacterDamage characterDamage;

    public float health=200;
    public float maxHealth = 200;

    public int no_of_hearts = 3;
    public int maxNoOfHeartsAllowed = 15;

    Vector2 moveInput;
    public float moveSpeed = 1000f;
    Rigidbody2D rb;
    SpriteRenderer spriteRenderer;
    bool move = true;
    public float maxSpeed = 5;
    private Animator animator;
    public float idleFriction = 0.9f;
    [SerializeField]
    private GameObject inventory;

    public GameObject deathHUD;
    
    

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterDamage = GetComponent<CharacterDamage>();
        health = characterDamage.Health;
    }

    private void FixedUpdate()
    {
        if(move && moveInput != Vector2.zero)
        {
            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);
            GetComponent<AudioSource>().UnPause();
            if (rb.velocity.magnitude > maxSpeed)
            {
                float speed = Mathf.Lerp(rb.velocity.magnitude,maxSpeed,idleFriction);
                rb.velocity = rb.velocity.normalized * speed;

            }
            if (moveInput.x > 0)
            {
                spriteRenderer.flipX = false;
                gameObject.BroadcastMessage("FacingRight", true);
            }
            else if (moveInput.x < 0)
            {
                spriteRenderer.flipX = true;
                gameObject.BroadcastMessage("FacingRight", false);
            }
            animator.SetBool("isMoving", true);
        }
        else
        {
            rb.velocity = Vector2.Lerp(rb.velocity,Vector2.zero,idleFriction);
            animator.SetBool("isMoving", false);
            GetComponent<AudioSource>().Pause();
        }
        
    }
    private void Update()
    {
        
        if (Input.GetKeyDown(KeyCode.M))
        {
            inventory.SetActive(true);
            if(no_of_hearts<=maxNoOfHeartsAllowed)
            no_of_hearts++;
           
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            inventory.SetActive(false);
            if(no_of_hearts<=maxNoOfHeartsAllowed)
            no_of_hearts--;
           
        }
        
        if (Input.GetKey(KeyCode.K))
            animator.SetTrigger("punch");

        health = characterDamage.Health;

        if(health <= 0) 
        {
            deathHUD.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed = 2500f;
           
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveSpeed = 1000f;
           
        }
       
    }

    private void OnMove(InputValue playerInput)
    {
        moveInput = playerInput.Get<Vector2>();
        if (moveInput != Vector2.zero)
        {
            animator.SetFloat("XInput", moveInput.x);
            animator.SetFloat("YInput", moveInput.y);
        }
    }
    public void Dead()
    {
        Time.timeScale = 0;
        GetComponent<AudioSource>().Pause();
    }
}
