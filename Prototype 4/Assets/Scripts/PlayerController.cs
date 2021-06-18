using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public PowerUpType currentPowerUp = PowerUpType.None;

    [Header("Smash")]
    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;

    private bool smashing = false;
    private float floorY;
    public float speed = 7f;
    public bool hasPowerUp;
    private float powerUpStrength = 12f;
    private Rigidbody playerRb;
    public GameObject rocketPrefab;
    private GameObject tmpRocket;
    private Coroutine powerUpCountDown;
    private GameObject powerUpIndicator;
    private GameObject focalPoint;
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("FocalPoint");
        powerUpIndicator = GameObject.Find("PowerUpIndicator");
        powerUpIndicator.SetActive(false);
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward* forwardInput * speed);
        powerUpIndicator.transform.position = transform.position - new Vector3(0f, .5f, 0f);
        if (currentPowerUp == PowerUpType.Rockets && Input.GetKeyDown(KeyCode.F))
        {
            LaunchRockets();
        }
        else if (currentPowerUp ==PowerUpType.Smash && Input.GetKeyDown(KeyCode.Space)&&!smashing)
        {
            smashing = true;
            StartCoroutine(Smash());
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            currentPowerUp = other.gameObject.GetComponent<PowerUp>().powerUpType;
            Destroy(other.gameObject);
            powerUpIndicator.SetActive(true);
            if (powerUpCountDown != null) 
            {
                StopCoroutine(powerUpCountDown);
            }
            powerUpCountDown = StartCoroutine(PowerUpCountDownRoutine());
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (((collision.gameObject.CompareTag("Enemy")&& hasPowerUp) && (currentPowerUp == PowerUpType.Pushback)) ||((collision.gameObject.CompareTag("EnemyStrong") && hasPowerUp)&&(currentPowerUp == PowerUpType.Pushback)))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 powerUpDirection = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(powerUpDirection * powerUpStrength, ForceMode.Impulse);
        }
    }
    private void LaunchRockets()
    {
        foreach (var enemy in FindObjectsOfType<EnemyController>())
        {
            tmpRocket = Instantiate(rocketPrefab, transform.position + Vector3.up, Quaternion.identity);
            tmpRocket.GetComponent<RocketBehaviour>().Fire(enemy.transform);
        }
    }

    IEnumerator PowerUpCountDownRoutine()
    {
        yield return new WaitForSeconds(5f);
        hasPowerUp = false;
        currentPowerUp = PowerUpType.None;
        powerUpIndicator.SetActive(false);
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<EnemyController>();
        floorY = transform.position.y;
        float jumpTime = Time.time + hangTime;

        while (Time.time < jumpTime)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
            yield return null;
        }
        while (transform.position.y > floorY)
        {
            playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
            yield return null;
        }

        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i] != null)
            {
                enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
            }
        }
        smashing = false;
    }
}
