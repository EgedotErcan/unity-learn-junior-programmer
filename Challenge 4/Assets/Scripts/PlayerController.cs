using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PowerUpType currentPowerUpType = PowerUpType.None;
    private bool smashing = false;
    public float explosionRadius;
    public float explosionForce;
    public float smashSpeed;
    public float hangTime;
    public float playerSpeed;
    public float turboBoost;
    private Rigidbody playerRb;
    public GameObject rocket;
    private GameObject tmpRocket;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    public ParticleSystem smokeParticle;
    private Coroutine powerUpCount;
    public bool hasPowerUp;
    public int powerUpDuration;
    private float normalStrength = 10f;
    private float powerUpStrength = 20f;
    private float floarY;
    

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * playerSpeed * verticalInput * Time.deltaTime);
        powerUpIndicator.transform.position = transform.position - new Vector3(0f,.5f,0f);
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playerRb.AddForce(focalPoint.transform.forward * turboBoost, ForceMode.Impulse);
            smokeParticle.Play();
        }
        else if (Input.GetKeyDown(KeyCode.F) && currentPowerUpType == PowerUpType.Attract)
        {
            LaunchRocket();
        }
        else if (Input.GetKeyDown(KeyCode.Q)&& currentPowerUpType == PowerUpType.Smash&&!smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }

    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<EnemyController>();
        floarY = transform.position.y;
        float jumpTime = Time.time + hangTime;
        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        while (transform.position.y>floarY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -(smashSpeed*2));
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i]!=null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.05f, ForceMode.Impulse);
            }
        }
        smashing = false;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            currentPowerUpType = other.gameObject.GetComponent<PowerUp>().powerUpType;
            powerUpIndicator.SetActive(true);
            if (powerUpCount != null)
            {
                StopCoroutine(powerUpCount);
            }
            powerUpCount = StartCoroutine(powerUpCountDown());
        }
    }
    IEnumerator powerUpCountDown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        currentPowerUpType = PowerUpType.None;
        powerUpIndicator.SetActive(false);
    }   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.transform.position- transform.position; 
            if (hasPowerUp && currentPowerUpType == PowerUpType.Pushback)
            {
                enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRb.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
    private void LaunchRocket()
    {
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            tmpRocket = Instantiate(rocket, transform.position + (Vector3.up) + (Vector3.forward*3), rocket.transform.rotation);
            tmpRocket.GetComponent<RocketBehavior>().Fire(enemy.transform);
        }
    }
}
