using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public GameObject[] powerUpPrefabs;
    public GameObject[] miniEnemyPrefabs;
    public GameObject bossPrefab;
    private float spawnRange = 9f;
    public int enemyCount;
    public int waweNumber;
    public int bossRound;
    

    // Start is called before the first frame update
    void Start()
    {
        SpawnEnemyWave(waweNumber);
        int randomPowerup = Random.Range(0, powerUpPrefabs.Length);
        Instantiate(powerUpPrefabs[randomPowerup], GenerateSpawnPosition(), powerUpPrefabs[randomPowerup].transform.rotation); 


    }
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            waweNumber++;
            if (waweNumber % bossRound == 0)
            {
                SpawnBossWave(waweNumber);
            }
            else
            {
                SpawnEnemyWave(waweNumber);
            }
            int randomPowerup = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomPowerup], GenerateSpawnPosition(), powerUpPrefabs[randomPowerup].transform.rotation);
        }
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemyPrefab.Length);
            Instantiate(enemyPrefab[randomIndex], GenerateSpawnPosition(), enemyPrefab[randomIndex].transform.rotation);
        }
        ResetPlayerPosition();
    }

    private void ResetPlayerPosition()
    {
        GameObject.Find("Player").transform.position = new Vector3(0.04f, .15f, 0f);
        GameObject.Find("Player").GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, .1f, spawnPosZ);

        return randomPos;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, miniEnemyPrefabs.Length);
            Instantiate(miniEnemyPrefabs[randomMini], GenerateSpawnPosition(), miniEnemyPrefabs[randomMini].transform.rotation);
        }
    }
    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<EnemyController>().miniEnemySpawnCount = miniEnemysToSpawn;
        ResetPlayerPosition();
    }
}
