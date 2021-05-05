using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] animalPrefabs;
    private float spawnRangeX = 13f;
    private float spawnPosZ = 20f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int animalIndex = Random.Range(0, animalPrefabs.Length);
        Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX,spawnRangeX), 0, spawnPosZ);

        if (Input.GetKeyDown(KeyCode.S))
        {
            Instantiate(animalPrefabs[animalIndex], spawnPosition, animalPrefabs[animalIndex].transform.rotation);
        }
    }
}
