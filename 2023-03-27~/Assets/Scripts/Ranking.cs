using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public Text[] rankText;
    public Text myRankTextN;
    public Text myRankTextS;
    public InputField inputField;

    public int[] rankScore;
    public int myRankScore;
    public int tempRankScore;
    public int curRankScore;
    public int playerRank;

    public string[] rankName;
    public string myRankName;
    public string tempRankName;
    public string curRankName;

    public bool isWrite;
    private void Awake()
    {
        myRankScore = Player.score;
        curRankScore = Player.score;
        Rank();
    }
    void Rank()
    {
        for (int i = 0; i < 5; i++)
        {
            rankText[i].text = $"{i + 1}등 이름 : {rankName[i]} 점수 : {rankScore[i]}";
        }
        if (SceneManager.GetActiveScene().name == "Finish")
        {
            myRankTextN.text = $"이름 : {myRankName}";
            myRankTextS.text = $"점수 : {myRankScore}";
        }
    }
    public void RankUpload()
    {
        if (!isWrite && Regex.IsMatch(inputField.text, @"^[A-Z]+$") && inputField.text.Length == 3)
        {
            curRankName = inputField.text;
            for (int i = 0; i < 5; i++)
            {
                if (rankScore[i] < myRankScore)
                {
                    isWrite = true;
                    RankCompare(i);
                    return;
                }
            }
        }
        else
        {
            inputField.text = "";
            myRankName = inputField.text;
        } 
    }
    void RankCompare(int i)
    {

        for (int j = i; j < 5; j++)
        {
            tempRankScore = rankScore[j];
            rankScore[j] = curRankScore;
            curRankScore = tempRankScore;

            tempRankName = rankName[j];
            rankName[j] = curRankName;
            curRankName = tempRankName;
        }
        Rank();
    }
}