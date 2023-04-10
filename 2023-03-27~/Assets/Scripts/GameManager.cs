using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpawnManager spawnManager;
    public SpriteRenderer background;
    public GameObject stageClearUI;
    public Sprite[] backgroundObjs;

    public float delay;

    static public int stageIndex;

    private void FixedUpdate()
    {
        delay += Time.deltaTime;
        if (Input.GetKey(KeyCode.F6) && delay > 2f)
        {
            StageClear();
            delay = 0;
        }
    }
    public void StageClear()
    {
        spawnManager.bossSpawn = false;
        if(stageIndex == 2)
        {
            GameOver();
        }
        else
        {
            stageClearUI.SetActive(true);
            Invoke("Sinario", 5f);
            stageIndex++;
            background.sprite = backgroundObjs[stageIndex];
            spawnManager.curSpawnDelay = -10f;
        }
    }
    
    public void Sinario()
    {
        stageClearUI.SetActive(false); 
    }

    public void GameOver()
    {
        stageIndex = 0;
        SceneManager.LoadScene("Finish");
    }
}
