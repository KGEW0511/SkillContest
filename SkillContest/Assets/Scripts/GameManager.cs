using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public GameObject gameClearUI;

    static public float stageIndex;
    void Start()
    {
        
    }
    void Update()
    {
        
    }

    public void StageClear()
    {
        if (stageIndex < 2)
        {
            gameClearUI.SetActive(true);
            spawnManager.curSpawnDelay = -5f;
            spawnManager.bossSpawn = false;
            stageIndex++;
            Invoke("Return", 5f);
        }
        else if(stageIndex == 2)
        {
            GameOver();
        }
    }

    void Return()
    {
        gameClearUI.SetActive(false);
    }
    public void GameOver()
    {
        SceneManager.LoadScene("Finish");
    }
}
