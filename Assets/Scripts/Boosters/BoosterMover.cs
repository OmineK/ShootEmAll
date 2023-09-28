using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoosterMover : MonoBehaviour
{
    [SerializeField] float boosterSpeed = 4.5f;

    Rigidbody2D objectRb;

    void Awake()
    {
        objectRb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        MoveObject();
    }

    void MoveObject()
    {
        Vector3 newPos = transform.position + Vector3.down * Time.deltaTime * boosterSpeed;

        objectRb.MovePosition(newPos);
    }
}
