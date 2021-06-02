using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float spawnRange = 9f;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(enemyPrefab,GenerateSpawnPosition(), enemyPrefab.transform.rotation);
    }
    void Update()
    {
        
    }

    private Vector3 GenerateSpawnPosition() 
    {
        float spawnPosX = Random.Range(-spawnRange, spawnRange);
        float spawnPosZ = Random.Range(-spawnRange, spawnRange);
        Vector3 randomPos = new Vector3(spawnPosX, .1f, spawnPosZ);

        return randomPos;
    }
}
