using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBehavior : MonoBehaviour
{
    private Transform target;
    private float aliveTimer=4f;
    private float rocketSpeed=25f;
    private float rocketStrength = 15f;

    private void Update()
    {
        if (target != null)
        {
            Vector3 moveDirection = (target.transform.position - transform.position).normalized;
            transform.position += Time.deltaTime * rocketSpeed * moveDirection;
            transform.LookAt(target);
        }
    }
    public void Fire(Transform newTarget)
    {
        target = newTarget;
        Destroy(gameObject, aliveTimer);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (target != null)
        {
            if (collision.gameObject.CompareTag("Enemy"))
            {
                Vector3 away = transform.position - collision.transform.position;
                Rigidbody enemyRb = collision.gameObject.GetComponent<Rigidbody>();
                enemyRb.AddForce(away * rocketStrength, ForceMode.Impulse);
                Destroy(gameObject);
            }
        }
    }
}
