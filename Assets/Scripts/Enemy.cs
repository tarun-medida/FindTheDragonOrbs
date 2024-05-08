using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float checkRadius;
    public float shootRadius;
    public float attackRadius;
    public CharacterDamage characterDamage;

    public bool shouldRotate;
    public LayerMask mask;


    private Transform target;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 movement;
    private Vector3 direction;
    private SpriteRenderer sprite;

    private bool isInChaseRange;
    private bool isInShootRange;
    private bool isInAttackRange;
    public float attackDamage = 1.0f;
    public GameObject winHud;

    public float fireRate;
    private float timeToFire = 0f;
    public Transform firePoint;
    public float bulletForce;
    public GameObject bulletPrefab;


    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
        sprite = GetComponent<SpriteRenderer>();
        characterDamage = GetComponent<CharacterDamage>();
    }

    private void Update()
    {
        animator.SetBool("isMoving", isInChaseRange);

        isInChaseRange = Physics2D.OverlapCircle(transform.position, checkRadius, mask);
        isInAttackRange = Physics2D.OverlapCircle(transform.position, attackRadius, mask);
        isInShootRange = Physics2D.OverlapCircle(transform.position, shootRadius, mask);

        direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        direction.Normalize();
        movement = direction;

        

        if (shouldRotate)
        {
            animator.SetFloat("X", direction.x);
            animator.SetFloat("Y", direction.y);
            if (direction.x < 0)
            {
                sprite.flipX = true;
            }
            if (direction.x > 0)
            {
                sprite.flipX = false;
            }
        }
        
        
        
        

    }
    private void FixedUpdate()
    {
        //if(isInChaseRange)
        //{
        //    MoveCharacter(movement);
        //}
        if (isInShootRange && !isInAttackRange)
        {
            
            if (Vector2.Distance(target.position, transform.position) <= shootRadius)
            {
                if (Vector2.Distance(target.position, transform.position) >= checkRadius)
                    Shoot();
            }
        }
        if (isInAttackRange)
        {
            animator.SetTrigger("Attack");
        }
    }



    private void Shoot()
    {

        if (firePoint != null)
        {
            if (timeToFire <= 0f)
            {
                animator.SetTrigger("Shoot");
                timeToFire = fireRate;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.transform.position, firePoint.transform.rotation);
                Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
                rb.AddForce(direction * bulletForce, ForceMode2D.Impulse);
                //audioSource.Play()
            }
            else
            {
                timeToFire -= Time.deltaTime;
            }
        }
    }


    //private void MoveCharacter(Vector2 dir)
    //{
    //    rb.MovePosition((Vector2) transform.position + (dir * speed * Time.deltaTime));
    //}

    //private void OnTriggerEnter2D(Collider2D collision)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        //if(collision.tag == "Player")
         {
                IDamageable damageableObject = collision.collider.GetComponent<IDamageable>();
                //IDamageable damageableObject = collision.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.Hit(attackDamage);
            }
        }
    }
}
