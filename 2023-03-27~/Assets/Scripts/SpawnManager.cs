using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] stage1EnemyObjs;
    public GameObject[] stage2EnemyObjs;
    public GameObject[] stage3EnemyObjs;
    public GameObject astroidObj;
    public GameObject[] bossObjs;

    public bool bossSpawn;

    public float curSpawnDelay;
    public float maxSpawnDelay;

    public int bossSpawnCount;
    private void FixedUpdate()
    {
        spawnEnemy();
    }
    void spawnEnemy()
    {
        curSpawnDelay += Time.deltaTime;
        if(curSpawnDelay >= maxSpawnDelay && !bossSpawn)
        {
            int enemyKind = Random.Range(0, 3);
            int enemyRange = Random.Range(-8, 9);
            int astroidRange = Random.Range(-8, 9);
            bossSpawnCount++;
            switch (GameManager.stageIndex)
            {
                case 0:
                    GameObject enemy1 = Instantiate(stage1EnemyObjs[enemyKind], new Vector2(enemyRange, 5), Quaternion.identity);
                    break;
                case 1:
                    GameObject enemy2 = Instantiate(stage2EnemyObjs[enemyKind], new Vector2(enemyRange, 5), Quaternion.identity);
                    break;
                case 2:
                    GameObject enemy3 = Instantiate(stage3EnemyObjs[enemyKind], new Vector2(enemyRange, 5), Quaternion.identity);
                    break;
            }
            GameObject astroid = Instantiate(astroidObj, new Vector2(astroidRange, 5), Quaternion.identity);
            curSpawnDelay = 0;
        }
        if(bossSpawnCount >= 60)
        {
            GameObject Boss = Instantiate(bossObjs[GameManager.stageIndex], new Vector2(0, 10), Quaternion.identity);
            bossSpawn = true;
            bossSpawnCount = 0;
        }
    }
}
