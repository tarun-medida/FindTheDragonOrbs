using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    public float attackDamage = 1.0f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        /*
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        */
        if (collision.gameObject.tag == "Player")
        //if(collision.tag == "Player")
        {
            IDamageable damageableObject = collision.collider.GetComponent<IDamageable>();
            //IDamageable damageableObject = collision.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.Hit(attackDamage);
                Destroy(gameObject);
            }
        }
        Destroy(gameObject, 5f);
    }

}
