using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject minion;
    public GameObject boss;
    public enum SpawnState { Spawning, Waiting, Counting};
    [System.Serializable]
    public class Wave
    {
        public string name;
        public int count;
        public float rate;
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
            if (_wave.name == "Final Wave" && i ==4)
            {
                Instantiate(boss, new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0), Quaternion.identity);
            }
            Instantiate(minion, new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0), Quaternion.identity);
            yield return new WaitForSeconds(1f/ _wave.rate);
        }


        state = SpawnState.Waiting;

        yield break;
    }
}
//public GameObject minionSpawner;


//public float spawnInterval = 3.5f;

//public int no_of_Waves = 3;
//public int no_of_Wave_Enemies = 5;
//public int enemiesMultiplier = 2;
//private int no_of_spawned_enemies = 0;

//// Start is called before the first frame update
//void Start()
//{
//    StartCoroutine(spawnMinion(spawnInterval, minionSpawner));
//}

//void Update()
//{

//}

//private IEnumerator spawnMinion(float interval,GameObject minion)
//{

//    while(no_of_Waves > 0)
//    {

//        if(no_of_spawned_enemies != no_of_Wave_Enemies)
//        {
//            for (int i = 0; i < no_of_Wave_Enemies; i++)
//            {
//                GameObject newMinion = Instantiate(minion, new Vector3(Random.Range(10f, -10f), Random.Range(10f, -10f), 0), Quaternion.identity);
//                yield return new WaitForSeconds(spawnInterval);
//                no_of_spawned_enemies++;
//                Debug.Log(i + 1);
//                Debug.Log(no_of_spawned_enemies);
//            }
//            Debug.Log(CharacterDamage.enemiesKilled);
//        }
//        if (CharacterDamage.enemiesKilled == no_of_spawned_enemies)
//        {
//            no_of_Wave_Enemies = no_of_Wave_Enemies * enemiesMultiplier;
//            Debug.Log("New wave starting");
//            no_of_Waves--;
//            yield return new WaitForSeconds(5f);
//        }
//    }
//    if(no_of_Waves == 0)
//    {
//        StopCoroutine(spawnMinion(spawnInterval, minionSpawner));
//        Debug.Log("Waves Ended");
//    }

//    yield break;
//}
