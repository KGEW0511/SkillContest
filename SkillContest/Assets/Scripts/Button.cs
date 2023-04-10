using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Button : MonoBehaviour
{
    public GameObject menuUI;
    public GameObject helpUI;
    public GameObject rankUI;

    public void OnClickRank()
    {
        menuUI.SetActive(false);
        rankUI.SetActive(true);
    }
    public void OnClickHelp()
    {
        menuUI.SetActive(false);
        helpUI.SetActive(true);
    }
    public void OnClickFirstScene()
    {
        SceneManager.LoadScene("Start");
    }
    public void OnClickFinish()
    {
        Application.Quit();
    }
    public void OnClickStage()
    {
        SceneManager.LoadScene("Stage");
    }
}
