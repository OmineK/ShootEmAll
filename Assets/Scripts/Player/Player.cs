using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] float moveSpeed = 7f;
    [SerializeField] int startingLifes = 2;
    [SerializeField] ParticleSystem playerExplodeVFX;

    ParticleSystem bullet;
    ParticleSystem.MainModule bulletParticleMainModule;

    AudioSource audioSource;
    Renderer shiledBody;
    UIHandler ui;

    readonly float moveRange = 8.4f;
    float horiInput;

    int currentLifes = 0;
    int currentMaximumBullets = 0;

    bool shieldIsActive = false;
    float shiledDuration = 0f;

    void Awake()
    {
        bullet = GetComponentInChildren<ParticleSystem>();
        bulletParticleMainModule = GetComponentInChildren<ParticleSystem>().main;

        audioSource = GetComponent<AudioSource>();
        shiledBody = GetComponentInChildren<EmptyShiledBody>().gameObject.GetComponent<Renderer>();
        shiledBody.enabled = false;

        currentLifes = startingLifes;
        currentMaximumBullets = bulletParticleMainModule.maxParticles;
    }

    void Start()
    {
        ui = FindObjectOfType<UIHandler>();

        ui.BulletTextUpdate(currentMaximumBullets);
        ui.LifeTextUpdate(currentLifes);
        ui.shiledText.enabled = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void FixedUpdate()
    {
        PlayerMove();
        MotionRotation();
    }

    void Update()
    {
        PlayerInputs();
        PlaySound();
        PlayerImmortality();
    }

    void PlayerMove()
    {
        horiInput = Input.GetAxis("Horizontal");
        float newPos = transform.localPosition.x + horiInput * Time.deltaTime * moveSpeed;

        float clampedNewPos = Mathf.Clamp(newPos, -moveRange, moveRange);

        transform.localPosition = new Vector3(clampedNewPos, transform.localPosition.y, transform.localPosition.z);
    }

    void MotionRotation()
    {
        float Yaw = horiInput * 35f;

        transform.localRotation = Quaternion.Euler(0f, Yaw, 0f);
    }

    void PlayerInputs()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            ui.pauseCanvas.SetActive(true);
            audioSource.Stop();
        }
    }

    void PlaySound()
    {
        if ((!audioSource.isPlaying) && Time.timeScale == 1)
            audioSource.Play();
    }

    void PlayerImmortality()
    {
        if (shiledDuration > 0)
        {
            shieldIsActive = true;
            shiledDuration -= Time.deltaTime;
            ui.shiledText.enabled = true;
            ui.shiledText.text = "Shiled time: " + shiledDuration.ToString("0.0");
            shiledBody.enabled = true;
        }
        else
        {
            shieldIsActive = false;
            ui.shiledText.enabled = false;
            shiledDuration = 0;
            shiledBody.enabled = false;
        }
    }

    void Shoot(bool isActive)
    {
        var bulletEmission = bullet.emission;
        bulletEmission.enabled = isActive;
    }

#region Collision Handler
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            if (!shieldIsActive)
            {
                currentLifes--;
                ui.LifeTextUpdate(currentLifes);

                enemy.EnemyDeath();

                Instantiate(playerExplodeVFX, transform.position, Quaternion.identity);

                this.gameObject.SetActive(false);

                Invoke(nameof(PlayerRes), 4f);
            }
            else
                enemy.EnemyDeath();
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BulletBoost"))
        {
            currentMaximumBullets++;
            bulletParticleMainModule.maxParticles = currentMaximumBullets;

            ui.BulletTextUpdate(currentMaximumBullets);

            collision.GetComponentInChildren<Renderer>().enabled = false;
            collision.GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject, 1f);
        }
        else if (collision.gameObject.CompareTag("ExtraLife"))
        {
            int addLifeAmount = collision.gameObject.GetComponent<ExtraLife>().ExtraLifeAddAmount;
            currentLifes += addLifeAmount;

            ui.LifeTextUpdate(currentLifes);

            collision.GetComponentInChildren<Renderer>().enabled = false;
            collision.GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject, 1f);
        }
        else if (collision.gameObject.CompareTag("ExtraShiled"))
        {
            shiledDuration += collision.gameObject.GetComponent<ExtraShiled>().ExtraShiledAddDuration;

            collision.GetComponentInChildren<Renderer>().enabled = false;
            collision.GetComponent<AudioSource>().Play();

            Destroy(collision.gameObject, 1f);
        }
    }

    void PlayerRes()
    {
        if (currentLifes > 0)
        {
            this.gameObject.SetActive(true);
            shiledDuration += 5f;
        }
        else
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        ui.gameOverCanvas.SetActive(true);
    }
    #endregion
}
