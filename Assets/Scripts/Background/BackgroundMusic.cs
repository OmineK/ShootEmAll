using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    void Awake()
    {
        int numCurrentMusicPlays = FindObjectsOfType<BackgroundMusic>().Length;

        if (numCurrentMusicPlays > 1)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
