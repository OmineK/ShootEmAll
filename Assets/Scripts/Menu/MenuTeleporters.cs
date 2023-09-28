using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuTeleporters : MonoBehaviour
{
    [SerializeField] Vector3 newPos = new(0, 0, 0);

    void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = newPos;
    }
}
