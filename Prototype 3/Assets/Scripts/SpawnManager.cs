using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] obstaclePrefabArray;
    private PlayerController playerControllerScript;
    private Vector3 spawnPos = new Vector3(23f,0f,0f);
    private float spawnInterval;
    private float timer;
    private float minInterval= 1.3f;
    private float maxInterval = 1.5f;


    void Start()
    { 
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        spawnInterval = 2f;
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer> spawnInterval)
        {
            timer = 0;
            SpawnObstacle();
            spawnInterval = Random.Range(minInterval, maxInterval);
        }

    }

    void SpawnObstacle()
    {
        int obstaclePrefabArrayIndex = Random.Range(0, obstaclePrefabArray.Length);
        
        if (playerControllerScript.gameOver == false)
        {
            Instantiate(obstaclePrefabArray[obstaclePrefabArrayIndex], spawnPos, obstaclePrefabArray[obstaclePrefabArrayIndex].transform.rotation);
        }

    }
}
