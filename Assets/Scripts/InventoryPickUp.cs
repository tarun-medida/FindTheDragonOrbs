using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPickUp : MonoBehaviour
{
    public GameObject imageAxe;
    public bool canPickUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Press P to pick up the weapon");
            canPickUp = true;
        }
    }

    private void Update()
    {
        if (canPickUp && Input.GetKeyDown(KeyCode.P)) 
        {
            Debug.Log("Picked Up Weapon");
            imageAxe.SetActive(true);
            Destroy(gameObject);
        }
    }
}
