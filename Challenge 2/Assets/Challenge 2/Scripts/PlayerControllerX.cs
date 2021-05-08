using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    public GameObject dogPrefab;

    private float timer;
    private float limitTime = 0.7f;


    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        // On spacebar press, send dog
        if (Input.GetKeyDown(KeyCode.Space) && timer > limitTime)
        {
            timer = 0;
            Instantiate(dogPrefab, transform.position, dogPrefab.transform.rotation);
        }
    }
}
