using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 3f;
    private float strongEnemyForce = 5f;
    private Rigidbody enemyRb;
    private GameObject player;
    private SpawnManager spawnManager;
    public int miniEnemySpawnCount;
    public bool isBoss = false;
    public float spawnInterval;
    private float nextSpawn;

    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        if (isBoss)
        {
            spawnManager = FindObjectOfType<SpawnManager>();
        }
    }

    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (isBoss)
        {
            if (Time.time > nextSpawn)
            {
                nextSpawn = Time.time + spawnInterval;
                spawnManager.SpawnMiniEnemy(miniEnemySpawnCount);
            }
        }

        if (transform.position.y < -20)
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("EnemyStrong") && collision.gameObject.CompareTag("Player"))
        {
            Vector3 lookDirection = player.transform.position - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(lookDirection * strongEnemyForce, ForceMode.Impulse);
        }
    }

}
