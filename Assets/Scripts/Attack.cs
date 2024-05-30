using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private float attackDamage;
    public Collider2D attackCollider;
    public Vector3 right = new Vector3(1.86f, 0f, 0f);
    public Vector3 left = new Vector3(-1.86f, 0f, 0f);
    public float knockbackForce = 5f;
    public bool isSpecialAttack;
    public bool isRadialDamage;


    // Start is called before the first frame update
    void Start()
    {
        if(attackCollider == null)
        {
            Debug.Log("Attack Collider not set");
        }
    }

    private void Update()
    {

        // basic attack
        if (isSpecialAttack == false && isRadialDamage == false)
            attackDamage = GameInstance.instance.getEquippedWeaponDamage();

        // sp1
        if (isSpecialAttack)
            attackDamage = 70;
        // sp2
        if (isRadialDamage)
            attackDamage = 50;
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
