using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject Menu;
    public GameObject Help;
    public GameObject Rank;
    public GameObject setting;

    public bool set;

    public void StartGameScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void ChoiceScene()
    {
        SceneManager.LoadScene("Choice");
    }
    public void StageScene()
    {
        SceneManager.LoadScene("Stage");
    }

    public void ViewHelp()
    {
        Menu.SetActive(false);
        Help.SetActive(true);
    }
    public void ViewRank()
    {
        Menu.SetActive(false);
        Rank.SetActive(true);
    }

    public void GameFinish()
    {
        Application.Quit();
    }

    public void Setting()
    {
        if (set)
        {
            Time.timeScale = 1;
            setting.SetActive(false);
            set = false;
        }
        else
        {
            Time.timeScale = 0;
            setting.SetActive(true);
            set = true;
        }
    }

}
