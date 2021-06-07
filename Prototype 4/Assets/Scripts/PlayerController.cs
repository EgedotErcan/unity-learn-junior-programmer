using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 7f;
    public bool hasPowerUp;
    private float powerUpStrength = 12f;
    private GameObject powerUpIndicator;
    private Rigidbody playerRb;
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

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PowerUp"))
        {
            hasPowerUp = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerUpCountDownRoutine());
            powerUpIndicator.SetActive(true);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy")&& hasPowerUp)
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 powerUpDirection = (collision.gameObject.transform.position - transform.position);

            enemyRb.AddForce(powerUpDirection * powerUpStrength, ForceMode.Impulse);
            
        }
    }
    IEnumerator PowerUpCountDownRoutine()
    {
        
        yield return new WaitForSeconds(5f);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }
}
