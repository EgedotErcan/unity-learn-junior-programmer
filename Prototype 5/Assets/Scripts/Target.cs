using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public int pointValue;
    private Rigidbody targetRb;
    private GameManager gameManager;
    public ParticleSystem explosionEffect;
    private float minSpeed = 12f;
    private float maxSpeed = 15f;
    private float xRange = 4f;
    private float ySpawn = -2f;
    private float maxTorque = 10f;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        transform.position = RandomPosition();
    }
    private void OnMouseDown()
    {
        Destroy(gameObject);
        Instantiate(explosionEffect, transform.position, explosionEffect.transform.rotation);
        gameManager.UpdateScore(pointValue);
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
    private Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }
    private Vector3 RandomPosition()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawn);
    }
    private float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }
}
