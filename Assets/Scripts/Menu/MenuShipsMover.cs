using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuShipsMover : MonoBehaviour
{
    [SerializeField] Vector3 moveDirection = new(0, 0, 0);

    float moveSpeed = 2f;

    void Update()
    {
        MoveShip();
    }

    void MoveShip()
    {
        transform.position += moveDirection * Time.deltaTime * moveSpeed;
    }
}
