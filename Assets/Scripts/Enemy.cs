using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    public float checkRadius;
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
    private bool isInAttackRange;
    public float attackDamage = 1.0f;
    public GameObject winHud;


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
        if(characterDamage.health <= 0)
        {
            Debug.Log("dead");
            winHud.SetActive(true);
            Destroy(gameObject);
            Time.timeScale = 0;
        }

    }
    private void FixedUpdate()
    {
        if(isInChaseRange && !isInAttackRange)
        {
            MoveCharacter(movement);
        }
        if (isInAttackRange)
        {
            animator.SetTrigger("Attack");
        }
    }

    private void MoveCharacter(Vector2 dir)
    {
        rb.MovePosition((Vector2) transform.position + (dir * speed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player"){
            IDamageable damageableObject = collision.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.Hit(attackDamage);
            }
        }
    }
}
