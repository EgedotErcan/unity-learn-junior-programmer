using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManagerX : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private float spawnLimitXLeft = -22f;
    private float spawnLimitXRight = 7f;
    private float spawnPosY = 30f;
    public float spawnInterval;
    private float timer;
    private int minTime = 2;
    private int maxTime= 6;

    // Start is called before the first frame update
    void Start()
    {
        spawnInterval = Random.Range(minTime,maxTime);
    }
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer>spawnInterval)
        {
            SpawnRandomBall();
            timer = 0;
            spawnInterval = Random.Range(minTime,maxTime);
        }
    }

    // Spawn random ball at random x position at top of play area
    void SpawnRandomBall ()
    {
        // Generate random ball index and random spawn position
        Vector3 spawnPos = new Vector3(Random.Range(spawnLimitXLeft, spawnLimitXRight), spawnPosY, 0);
        int ballIndex = Random.Range(0, ballPrefabs.Length);
 
        // instantiate ball at random spawn location
        Instantiate(ballPrefabs[ballIndex], spawnPos, ballPrefabs[ballIndex].transform.rotation);
    }

}
