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
    private float heartsTracker;
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

    public AudioSource walkSound;

    public int noOfHealthPortions;
    public GameObject HealthRegenObject;
    public TMP_Text healthRegenText;
    private Animator healtRegenAnimator;

    //special attack delay and time variables
    private float coolDownTime = 5f;
    private float coolDownTimer = 0.0f;
    private bool isInCoolDown = false;
    public Image specialAttackRegenTimerImage;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = rb.GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        characterDamage = GetComponent<CharacterDamage>();
        health = characterDamage.Health;
        healthRegenText.text = noOfHealthPortions.ToString();
        healtRegenAnimator = HealthRegenObject.GetComponent<Animator>();
        // at start what was the maximum number of hearts the player started with
        // later on when player purchases new health in inventory then the no_of_hearts will be changed
        heartsTracker = no_of_hearts;
        // initially player can use special attack with no cooldown
        specialAttackRegenTimerImage.fillAmount = 0.0f;
    }

    private void FixedUpdate()
    {
        //if(move && moveInput != Vector2.zero)
        if(moveInput != Vector2.zero)
            {
            rb.AddForce(moveInput * moveSpeed * Time.deltaTime);
            //GetComponent<AudioSource>().UnPause();
            walkSound.UnPause();
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
            walkSound.Pause();
        }
        
    }

    private void Update()
    {
        // keep track
        
        // debug function
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
        {
            animator.SetTrigger("punch");
        }

        // drink portion
        if(noOfHealthPortions <= 0)
        {
            healtRegenAnimator.SetBool("NoPortions", true);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (noOfHealthPortions > 0)
            {
                healtRegenAnimator.SetBool("NoPortions", false);
                // decrease the no of health portions
                if (getNoOfHeartsFromHealth(health) < heartsTracker)
                {
                    healtRegenAnimator.SetTrigger("Regen");
                    noOfHealthPortions -= 1;
                    healthRegenText.text = noOfHealthPortions.ToString();
                    characterDamage.Health += 20f;
                }
                else
                {
                    Debug.Log("Already full health");
                }
            }
        }

        //do special attack
        if (Input.GetKeyDown(KeyCode.F))
        {
            DoSpecialAttack();
        }

        if (isInCoolDown)
            ApplyCooldown();

        health = characterDamage.Health;

        if(health <= 0) 
        {
            deathHUD.SetActive(true);
        }
        // sprint/walk faster condition
        if (Input.GetKeyDown(KeyCode.Space))
        {
            moveSpeed = 2500f;
           
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            moveSpeed = 1000f;
           
        }
       
    }

    private bool DoSpecialAttack()
    {
        if(isInCoolDown == true)
        {
            // in cool down, cannot do speical attack
            return false;
        }
        else
        {
            // not in cool down
            isInCoolDown = true;
            // setting the timer with the cooldown value
            coolDownTimer = coolDownTime;
            return true;

        }
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
        Time.timeScale = 0;
        walkSound.Pause();
    }

    private int getNoOfHeartsFromHealth(float hearts)
    {
        hearts = (int)(health * no_of_hearts) / maxHealth;
        // using ceil to ensure that portion can only be used if one heart is totally gone.
        return (int)Mathf.Ceil(hearts);
    }
}
