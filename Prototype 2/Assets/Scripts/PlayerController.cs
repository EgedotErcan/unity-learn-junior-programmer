using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float leftRightSpeed = 20f;
    private float xRange = 12f;
    private float horizontalInput;

    public GameObject prjectilePrefab;

    

    void Start()
    {

    }

    void Update()
    {
        MovePlayerInBoundary();
        InstantiateObject();
    }

    //Launch a projectile from the player 
    private void InstantiateObject()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(prjectilePrefab, transform.position, prjectilePrefab.transform.rotation);
        }
    }
    //Keep the player in boundary
    private void MovePlayerInBoundary()
    {
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }
        horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * leftRightSpeed);
    }
}
