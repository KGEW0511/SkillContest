using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GameClearUI : MonoBehaviour
{
    public Text scoreText;
    public Text stageText;
    public Text timeText;

    private void OnEnable()
    {
        scoreText.text = string.Format("점수 : {0}", Player.score);
        stageText.text = string.Format("스테이지 : {0}", GameManager.stageIndex);
        timeText.text = string.Format("시간 : {0}분 {1}초", (int)PlayerUI.time / 60, (int)PlayerUI.time % 60);
    }
}
