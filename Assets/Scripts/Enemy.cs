using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] ParticleSystem explosionVFX;
    [SerializeField] int pointsAddValue = 1;
    [SerializeField] int enemyHits = 1;
    
    public float enemySpeed = 3f;

    ObjectSpawner objectSpawner;
    UIHandler ui;

    Rigidbody2D enemyRb;

    void Awake()
    {
        enemyRb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        objectSpawner = FindObjectOfType<ObjectSpawner>();
        ui = FindObjectOfType<UIHandler>();
    }

    void FixedUpdate()
    {
        EnemyMove();
    }

    void EnemyMove()
    {
        if (objectSpawner.Stage3EndGame)
            enemySpeed = objectSpawner.EnemySpeedBoost;

        Vector3 newPos = transform.position + Vector3.down * Time.deltaTime * enemySpeed;
        enemyRb.MovePosition(newPos);
    }

    void OnParticleCollision(GameObject other)
    {
        enemyHits--;

        if (enemyHits == 0)
            EnemyDeath();
    }

    public void EnemyDeath()
    {
        ui.PointTextUpdate(pointsAddValue);

        Instantiate(explosionVFX, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
}
