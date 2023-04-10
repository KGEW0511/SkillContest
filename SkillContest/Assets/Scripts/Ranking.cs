using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    public Text rank1;
    public Text rank2;
    public Text rank3;
    public Text rank4;
    public Text rank5;
    public Text myRank;

    public InputField rank;

    int playerRank;

    int cnt = 0;
    bool isWrite;

    string curN;
    public int curS;

    int tempS;
    string tempN;

    int[] rankScore = new int[5];
    string[] rankName = new string[5];
    void Start()
    {
        curS = Player.score;
        isWrite = false;
        myRank.text = string.Format("점수 : {0}", curS);
        Rank();
    }

    void Rank()
    {
        rank1.text = string.Format("1등 이름 : {0} 점수 : {1}", rankName[0], rankScore[0]);
        rank2.text = string.Format("2등 이름 : {0} 점수 : {1}", rankName[1], rankScore[1]);
        rank3.text = string.Format("3등 이름 : {0} 점수 : {1}", rankName[2], rankScore[2]);
        rank4.text = string.Format("4등 이름 : {0} 점수 : {1}", rankName[3], rankScore[3]);
        rank5.text = string.Format("5등 이름 : {0} 점수 : {1}", rankName[4], rankScore[4]);
    }

    public void Upload()
    {
        curN = rank.text;
        if (!isWrite)
        {
            isWrite = true;

            for (int i = 0; i < 5; i++)
            {
                if (curS > rankScore[i])
                {
                    RankSys(i);
                }
            }
        }
        else
        {
            rankName[playerRank] = rank.text;
        }
        Rank();
        myRank.text = string.Format("이름 : {0} 점수 : {1}", rank.text, Player.score);
    }

    void RankSys(int n)
    {
        playerRank = n;
        for (int i = n; i < 5; i++)
        {
            tempS = curS;
            curS = rankScore[i];
            rankScore[i] = tempS;

            tempN = curN;
            curN = rankName[i];
            rankName[i] = tempN;
        }
    }
}
