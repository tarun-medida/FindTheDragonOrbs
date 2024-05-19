using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject minion;
    public Transform[] spawnPoints;
    [SerializeField] public float spawnRate;
    public enum SpawnState { Spawning, Waiting, Counting};
    SpawnState state;

    //private float searchCountdown = 1f;


    public void SpawnEnemies()
    {
        //StartCoroutine(SpawnWave());
        state = SpawnState.Spawning;
        StartCoroutine(SpawnEnemiesBasedOnThreshold());
        this.GetComponent<AudioSource>().Play();
    }

    private void Update()
    {
        /*
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                Debug.Log("Spawning is completed.");
                Destroy(gameObject);

            }
            else
            {
                return;
            }
        }
        */
    }

    /*
    bool EnemyIsAlive()
    {
        searchCountdown -= Time.deltaTime;
        if(searchCountdown <= 0)
        {
            searchCountdown = 1f;
            if (GameObject.FindGameObjectWithTag("Minion") == null)
            {
                return false;
            }
        }
        return true;
    }
    */
/*
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if(state != SpawnState.Spawning)
            {
                // play audio
                this.GetComponent<AudioSource>().Play();
                StartCoroutine(SpawnWave());
                GetComponent<Collider2D>().enabled = false;
            }
        }
    }
*/

    IEnumerator SpawnWave()
    {
        state = SpawnState.Spawning;
        
        //for (int i = 0;i < count ;i++)
        for (int i = 0;i < GameManager.instance.numberOfMinionsToSpawn ;i++)
            {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(minion, spawnPoints[randSpawnPoint].position, Quaternion.identity);
            if (i == GameManager.instance.numberOfMinionsToSpawn - 1)
            {

                //GameManager.instance.numberOfMinionsToSpawn *= 2;
                Debug.Log("All enemies spawned");
            }
            yield return new WaitForSeconds(1f/ spawnRate);
           
        }


        state = SpawnState.Waiting;

        yield break;
    }


    IEnumerator SpawnEnemiesBasedOnThreshold()
    {
        while (true)
        {
            if (GameManager.instance.numberOfEnemiesSpanwed == GameManager.instance.numberOfMinionsToSpawn)
            {
                state = SpawnState.Waiting;
                yield return new WaitForSeconds(1f / spawnRate);
            }
            // enemies spanwed is less than threshold
            else
            {
                Debug.Log("Spawning....");
                state = SpawnState.Spawning;
                int randSpawnPoint = Random.Range(0, spawnPoints.Length);
                Instantiate(minion, spawnPoints[randSpawnPoint].position, Quaternion.identity);
                GameManager.instance.numberOfEnemiesSpanwed += 1;
                yield return new WaitForSeconds(1f / spawnRate);
            }
        }
    }
}

