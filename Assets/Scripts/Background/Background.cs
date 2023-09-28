using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] float scrollSpeed = 0.04f;

    Renderer background;

    void Awake()
    {
        background = GetComponent<Renderer>();
    }

    void Update()
    {
        background.material.mainTextureOffset += new Vector2 (0, scrollSpeed * Time.deltaTime);
    }
}
