using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject minion;
    //public GameObject boss;
    public Transform[] spawnPoints;
    [SerializeField] public int count;
    [SerializeField] public float spawnRate;
    public enum SpawnState { Spawning, Waiting, Counting};
    //[System.Serializable]
    //public class Wave
    //{
    //    public string name;
    //    public int count;
    //    public float rate;
    //}

    //public Wave[] waves;
    //private int nextWave = 0;
    //public float timeBetweenWaves = 5f;

    //public float waveCountDown;
    public SpawnState state = SpawnState.Counting;

    private float searchCountdown = 1f;
    [SerializeField] public GameObject areaBlocker;


    private void Start()
    {
        //waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                Debug.Log("Spawning is completed.");
                Destroy(gameObject);
                Destroy(areaBlocker);
                //state = SpawnState.Counting;
                //waveCountDown = timeBetweenWaves;
                //if(nextWave+1 > waves.Length - 1)
                //{
                //    return;
                //}
                //else
                //{
                //    nextWave++;
                //}

            }
            else
            {
                return;
            }
        }
        //if (waveCountDown <= 0)
        //{
        //    if (state != SpawnState.Spawning)
        //    {
        //        StartCoroutine(SpawnWave(waves[nextWave]));
        //    }
        //}
        //else
        //{
        //    waveCountDown -= Time.deltaTime;
        //}
    }
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
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    Destroy(gameObject);
    //}

    IEnumerator SpawnWave()
    {
        state = SpawnState.Spawning;
        
        for (int i = 0;i < count ;i++)
        {
            int randSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(minion, spawnPoints[randSpawnPoint].position, Quaternion.identity);
            if (i == count)
            {
                Debug.Log("All enemies spawned");
                Destroy(gameObject);
            }
            yield return new WaitForSeconds(1f/ spawnRate);
           
        }


        state = SpawnState.Waiting;

        yield break;
    }
}

