using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawningEnemies : MonoBehaviour
{
    public EnemySpawner spawner;

    private void OnTriggerExit2D(Collider2D collision)
    {

        if(collision.tag == "Player")
        {
            Debug.Log("Entered");
            spawner.SpawnEnemies();
            Destroy(gameObject);
        }
    }
}
