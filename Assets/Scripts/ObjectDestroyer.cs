using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            float randomPosition = Random.Range(-8.4f, 8.4f);
            Vector3 teleportPos = new(randomPosition, 5.5f, 0);

            collision.gameObject.transform.position = teleportPos;
        }
        else
            Destroy(collision.gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(collision.gameObject);
    }
}
