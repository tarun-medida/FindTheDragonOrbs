using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    //Script used for dealing damage to the player.

    public float attackDamage = 5.0f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        //if(collision.tag == "Player")
        {
            IDamageable damageableObject = collision.collider.GetComponent<IDamageable>();
            //IDamageable damageableObject = collision.GetComponent<IDamageable>();
            if (damageableObject != null)
            {
                damageableObject.Hit(attackDamage);
                Debug.Log("hit hit hit");
            }
        }
    }
}
