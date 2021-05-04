using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float movingSpeed = 30f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveObjectForward();
    }

    private void MoveObjectForward()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * movingSpeed);
    }
}
