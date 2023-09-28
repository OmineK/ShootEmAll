using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    [Header("Object to spawn")]
    [SerializeField] GameObject enemy;
    [SerializeField] GameObject superEnemy1;
    [SerializeField] GameObject superEnemy2;
    [SerializeField] GameObject bulletBoost;
    [SerializeField] GameObject extraLife;
    [SerializeField] GameObject extraShiled;

    [Header("Time between object spawn")]
    [SerializeField] float enemySpawnSpeed = 1f;
    [SerializeField] float bulletBoostSpawnSpeed = 14f;
    [SerializeField] float extraLifeSpawnSpeed = 74f;
    [SerializeField] float extraShiledSpawnSpeed = 39f;

    public bool Stage3EndGame = false;
    public float EnemySpeedBoost = 0f;

    bool stage1 = false;
    bool stage2 = false;
    bool enemySpeedRead = false;

    float enemySpeed;
    float roundTimer;

    void Start()
    {
        StartCoroutine(ObjectSpawn(enemy, enemySpawnSpeed));
        StartCoroutine(ObjectSpawn(bulletBoost, bulletBoostSpawnSpeed));
        StartCoroutine(ObjectSpawn(extraLife, extraLifeSpawnSpeed));
        StartCoroutine(ObjectSpawn(extraShiled, extraShiledSpawnSpeed));

        StartCoroutine(SuperEnemySpawn(enemySpawnSpeed));
    }

    void Update()
    {
        roundTimer += Time.deltaTime;

        if (!Stage3EndGame)
            IncreaseDifficulty();
        else
        {
            while (!enemySpeedRead)
            {
                enemySpeed = FindObjectOfType<Enemy>().enemySpeed;

                if (enemySpeed == 0)
                    break;

                EnemySpeedBoost = enemySpeed;
                enemySpeedRead = true;
            }
        }
        
        if (Stage3EndGame && enemySpeedRead && (EnemySpeedBoost <= 9f))
        {
            ObjectSpeedMultiplier();
        }
    }

    IEnumerator ObjectSpawn(GameObject newGO, float spawnTime)
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnTime);
            SpawnNewObject(newGO);
        }
    }

    IEnumerator SuperEnemySpawn(float spawnTime)
    {
        while (true)
        {
            if (stage2 && !Stage3EndGame)
            {
                SpawnNewObject(superEnemy2);
            }
            else if (Stage3EndGame)
            {
                int whichEnemy = Random.Range(1, 5);

                if (whichEnemy == 1)
                    SpawnNewObject(superEnemy1);
                else if (whichEnemy == 2)
                    SpawnNewObject(superEnemy2);
                else if (whichEnemy == 3)
                    SpawnNewObject(superEnemy2);
                else if (whichEnemy == 4)
                    SpawnNewObject(superEnemy2);
            }

            yield return new WaitForSeconds(spawnTime);
        }
    }  

    //increase the difficulty over time
    void IncreaseDifficulty()
    {
        if ((roundTimer > 15f) && !stage1)
        {
            enemySpawnSpeed /= 3;
            stage1 = true;
        }
        else if ((roundTimer > 40f) && !stage2)
        {
            enemySpawnSpeed /= 5;
            stage2 = true;
        }
        else if ((roundTimer > 75f) && !Stage3EndGame)
        {
            enemySpawnSpeed = 0.00001f;
            Stage3EndGame = true;
        }
    }

    void SpawnNewObject(GameObject newGameObject)
    {
        float randomPosition = Random.Range(-8.4f, 8.4f);
        Vector3 spawnPos = new(randomPosition, 0, 0);

        Instantiate(newGameObject, transform.position + spawnPos, Quaternion.identity);
    }

    void ObjectSpeedMultiplier()
    {
        EnemySpeedBoost += 0.04f * Time.deltaTime;
    }
}
