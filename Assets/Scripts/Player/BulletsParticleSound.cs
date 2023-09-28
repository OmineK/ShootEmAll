using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletsParticleSound : MonoBehaviour
{
    [SerializeField] AudioClip bulletSFX;

    int bulletsAlive = 0;

    ParticleSystem bulletsParticle;
    AudioSource bulletSource;

    void Awake()
    {
        bulletsParticle = GetComponent<ParticleSystem>();
        bulletSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PlayBulletSFX();
    }

    void PlayBulletSFX()
    {
        if (bulletsParticle.particleCount > bulletsAlive)
        {
            bulletSource.PlayOneShot(bulletSFX);
        }

        bulletsAlive = bulletsParticle.particleCount;
    }
}
