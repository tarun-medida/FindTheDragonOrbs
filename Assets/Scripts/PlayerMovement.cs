using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static CoinPick;

public class PlayerMovement : MonoBehaviour 
{
    CharacterDamage characterDamage;
    public float health=200;
    public float maxHealth = 200;
    public int no_of_hearts = 3;
    public int maxNoOfHeartsAllowed = 15;

    public Vector2 moveInput;
    public float moveSpeed = 2500f;
    Rigidbody2D playerRB;
    public SpriteRenderer spriteRenderer;
    public float maxSpeed = 5;
    private Animator animator;
    public float idleFriction = 0.9f;
    public AudioSource walkSound, specialAttackSFX;
    
    //special attack delay and time variables
    private readonly float coolDownTime = 5f;
    private float coolDownTimer = 0.0f;
    private bool isInCoolDown = false;
    public Image specialAttackRegenTimerImage;
    public SpecialAttack attack;

    //dash ability parameters


    private void Start()
    {
        // getting player object's rigid body component.
        playerRB = GetComponent<Rigidbody2D>();
        spriteRenderer = playerRB.GetComponent<SpriteRenderer>();
        // getting player's animator component trigger animations based on key press.
        animator = GetComponent<Animator>();
        // getting damage script where damage actions are computed and updated
        characterDamage = GetComponent<CharacterDamage>();
        //setting player health for easier acces
        health = characterDamage.Health;
        // initially player can use special attack with no cooldown
        specialAttackRegenTimerImage.fillAmount = 0.0f;

    }

    private void FixedUpdate()
    {
        //if(move && moveInput != Vector2.zero)
        if(moveInput != Vector2.zero )
            {
            playerRB.AddForce(Time.deltaTime * moveInput * moveSpeed);
            walkSound.UnPause();
            if (playerRB.velocity.magnitude > maxSpeed)
            {
                float speed = Mathf.Lerp(playerRB.velocity.magnitude,maxSpeed,idleFriction);
                playerRB.velocity = playerRB.velocity.normalized * speed;

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
            playerRB.velocity = Vector2.Lerp(playerRB.velocity,Vector2.zero,idleFriction);
            animator.SetBool("isMoving", false);
            walkSound.Pause();
        }
        //for updating the direction in special attack
        if(moveInput.x > 0)
        {
            attack.GetMoveInput(1f, 0f);
        }
        if(moveInput.y > 0)
        {
            attack.GetMoveInput(0f, 1f);
        }
        if (moveInput.x < 0)
        {
            attack.GetMoveInput(-1f, 0f);
        }
        if (moveInput.y < 0)
        {
            attack.GetMoveInput(0f, -1f);
        }

    }

    private void Update()
    {
        // keep track

        // debug function
        /*
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
        */

        // attack
        if (Input.GetKey(KeyCode.K))
        {
            animator.SetTrigger("punch");
        }

        // drink portion
        if (Input.GetKeyDown(KeyCode.X))
        {
            GameManager.instance.DrinkPortion();
        }

        //do special attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            DoSpecialAttackAnim();
        }

        if (isInCoolDown)
            ApplyCooldown();

        health = characterDamage.Health;

        // sprint/walk faster condition
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            moveSpeed = 5000f;

        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            moveSpeed = 2500f;

        }
        
       
    }

    private bool DoSpecialAttackAnim()
    {
        if(isInCoolDown == true)
        {
            // in cool down, cannot do speical attack
            return false;
        }
        else
        {
            // perform special attack
            // not in cool down
            DoSpecialAttack();
            isInCoolDown = true;
            // setting the timer with the cooldown value
            coolDownTimer = coolDownTime;
            return true;

        }
    }

    private void DoSpecialAttack()
    {
        specialAttackSFX.Play();
        attack.SpecialBeamAttack();
        return;
    }

    private void ApplyCooldown()
    {
        coolDownTimer -= Time.deltaTime;
        if(coolDownTimer < 0.0f)
        {
            isInCoolDown= false;
            specialAttackRegenTimerImage.fillAmount = 0.0f;
        }
        else
        {
            specialAttackRegenTimerImage.fillAmount = coolDownTimer / coolDownTime;
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
        walkSound.Pause();
        Time.timeScale = 0;   
    }
    
    public void UpdateHealth()
    {
        characterDamage.Health += 20f;
    }
}
