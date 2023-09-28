using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruction : MonoBehaviour
{
    [SerializeField] float gameObjectDestroyTime = 3f;

    void Update()
    {
        Destroy(this.gameObject, gameObjectDestroyTime);
    }
}
