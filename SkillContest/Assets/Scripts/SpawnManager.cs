using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyObjs;
    public GameObject[] bossObjs;

    public int bossSpawnCount;

    public float curSpawnDelay;
    public float maxSpawnDelay;

    public bool bossSpawn;
    void Start()
    {
        bossSpawn = false;
    }

    void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay > maxSpawnDelay && !bossSpawn)
        {
            float enemyRange = Random.Range(-10f, 10f);
            int enemyKind = Random.Range(0, 3);

            GameObject emeny = Instantiate(enemyObjs[enemyKind], new Vector3(enemyRange, 10, 0), Quaternion.Euler(0, 0, 0));
            curSpawnDelay = 0;
            bossSpawnCount++;
        }
        
        if(bossSpawnCount >= 60)
        {
            GameObject boss = Instantiate(bossObjs[(int)GameManager.stageIndex], new Vector3(0, 15, 0), Quaternion.Euler(0, 0, 0));
            bossSpawnCount = 0;
            bossSpawn = true;
        }
    }
}