using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private float spawnRangeX = 19;
    private float spawnZMin=7f;
    private float spawnZMax=25f;

    public GameObject enemyPrefab;
    public GameObject player;
    public GameObject[] powerUpPrefabs;
    public int waveNumber;
    public int enemyCount;
    // Start is called before the first frame update
    void Start()
    {
        spawnEnemyWave(waveNumber);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsOfType<EnemyController>().Length;
        if (enemyCount == 0)
        {
            spawnEnemyWave(waveNumber);
        }
    }
    private void spawnEnemyWave(int enemiesToSpawn)
    {
        if (GameObject.FindGameObjectsWithTag("PowerUp").Length == 0)
        {
            int randomIndex = Random.Range(0, powerUpPrefabs.Length);
            Instantiate(powerUpPrefabs[randomIndex], GenerateSpawnPos(0) - new Vector3(0f, 0f, 15f), powerUpPrefabs[randomIndex].transform.rotation);
        }

        for (int i = 0; i < enemiesToSpawn; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPos(1), enemyPrefab.transform.rotation);
        }
        waveNumber++;
        ResetPlayerPosition();
    }

    private void ResetPlayerPosition()
    {
        player.transform.position = new Vector3(0f, 0f, -7f);
        player.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
    }

    private Vector3 GenerateSpawnPos(int spawnY)
    {
        float spawnX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnZ = Random.Range(spawnZMin, spawnZMax);
        Vector3 randomPos = new Vector3(spawnX, spawnY, spawnZ);
        return randomPos;
    }
    
}
