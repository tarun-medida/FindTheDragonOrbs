using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject minion;
    //public GameObject boss;
    
    public enum SpawnState { Spawning, Waiting, Counting};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
        public Transform area;
    }

    public Wave[] waves;
    private int nextWave = 0;
    public float timeBetweenWaves = 5f;

    public float waveCountDown;
    public SpawnState state = SpawnState.Counting;

    private float searchCountdown = 1f;


    private void Start()
    {
        waveCountDown = timeBetweenWaves;
    }

    private void Update()
    {
        if (state == SpawnState.Waiting)
        {
            if (!EnemyIsAlive())
            {
                Debug.Log(waves[nextWave].name + " is completed.");
                state = SpawnState.Counting;
                waveCountDown = timeBetweenWaves;
                if(nextWave+1 > waves.Length - 1)
                {
                    return;
                }
                else
                {
                    nextWave++;
                }

            }
            else
            {
                return;
            }
        }
        if (waveCountDown <= 0)
        {
            if (state != SpawnState.Spawning)
            {
                StartCoroutine(SpawnWave(waves[nextWave]));
            }
        }
        else
        {
            waveCountDown -= Time.deltaTime;
        }
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


    IEnumerator SpawnWave(Wave _wave)
    {
        state = SpawnState.Spawning;
        
        for (int i = 0;i < _wave.count;i++)
        {
            //if (_wave.name == "Wave 1" && i == 4)
            //{
            //    Instantiate(boss, new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0), Quaternion.identity);
            //}
            Instantiate(minion, new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0), Quaternion.identity);
            yield return new WaitForSeconds(1f/ _wave.rate);
        }


        state = SpawnState.Waiting;

        yield break;
    }
}

