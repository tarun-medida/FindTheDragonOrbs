using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackDamage = 5f;
    public Collider2D attackCollider;
    public Vector3 right = new Vector3(1.86f, 0f, 0f);
    public Vector3 left = new Vector3(-1.86f, 0f, 0f);
    public float knockbackForce = 5f;


    // Start is called before the first frame update
    void Start()
    {
        if(attackCollider == null)
        {
            Debug.Log("Attack Collider not set");
        }
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        IDamageable damageableObject = col.GetComponent<IDamageable>();
        if (damageableObject != null)
        {
            //Vector3 parentPosition = gameObject.GetComponentInParent<Transform>().position;
            Vector3 parentPosition = transform.parent.position;
            Vector2 direction = (Vector2)(col.transform.position - parentPosition).normalized;
            Vector2 knockback = direction * knockbackForce;

            damageableObject.Hit(attackDamage, knockback);
        }
    }

    void FacingRight(bool facingRight)
    {
        if (facingRight)
        {
            gameObject.transform.localPosition = right;
        }
        else
        {
           gameObject.transform.localPosition = left;
        }

    }
}
