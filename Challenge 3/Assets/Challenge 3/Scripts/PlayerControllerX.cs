using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public bool gameOver;
    public float floatForce;
    public float gravityModifier;
    public bool isLowEnough;
    
    private Rigidbody playerRb;
    private AudioSource playerAudio;

    public ParticleSystem explosionParticle;
    public ParticleSystem fireworksParticle;

    public AudioClip moneySound;
    public AudioClip explodeSound;
    public AudioClip bounceSound;


    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity *= gravityModifier;
        playerAudio = GetComponent<AudioSource>();
        playerRb = GetComponent<Rigidbody>();
        playerRb.AddForce(Vector3.up * 5,ForceMode.Impulse );

        // Apply a small upward force at the start of the game

    }

    // Update is called once per frame
    void Update()
    {
        // While space is pressed and player is low enough, float up
        if (Input.GetKey(KeyCode.Space) && !gameOver && isLowEnough)
        {
            playerRb.AddForce(Vector3.up * floatForce * Time.deltaTime);
        }
        if (transform.position.y > 12f)
        {
            isLowEnough = false;
        }else
        {
            isLowEnough = true;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        // if player collides with bomb, explode and set gameOver to true
        if (other.gameObject.CompareTag("Bomb"))
        {
            explosionParticle.Play();
            playerAudio.PlayOneShot(explodeSound, 1.0f);
            gameOver = true;
            Debug.Log("Game Over!");
            Destroy(other.gameObject);
        } 

        // if player collides with money, fireworks
        else if (other.gameObject.CompareTag("Money"))
        {
            fireworksParticle.Play();
            playerAudio.PlayOneShot(moneySound, 1.0f);
            Destroy(other.gameObject);

        }
        else if (other.gameObject.CompareTag("Ground") && !gameOver)
        {
            playerRb.AddForce(Vector3.up * 10, ForceMode.Impulse);
            playerAudio.PlayOneShot(bounceSound, 1.5f);
        }

    }
}
