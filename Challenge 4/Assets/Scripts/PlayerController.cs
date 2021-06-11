using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float playerSpeed;
    public float turboBoost;
    private Rigidbody playerRb;
    private GameObject focalPoint;
    public GameObject powerUpIndicator;
    public ParticleSystem smokeParticle;
    public bool hasPowerUp;
    public int powerUpDuration;
    private float normalStrength = 10f;
    private float powerUpStrength = 20f;
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

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PowerUp"))
        {
            Destroy(other.gameObject);
            hasPowerUp = true;
            powerUpIndicator.SetActive(true);
            StartCoroutine(powerUpCountDown());
        }
    }
    IEnumerator powerUpCountDown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerUp = false;
        powerUpIndicator.SetActive(false);
    }   
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = collision.transform.position- transform.position   ; 
            if (hasPowerUp)
            {
                enemyRb.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            }
            else
            {
                enemyRb.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }
}
