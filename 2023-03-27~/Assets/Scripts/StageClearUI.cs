using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StageClearUI : MonoBehaviour
{

    public Text scoreText;
    public Text stageIndexText;
    public Text timeText;

    public float scoreTemp;
    public float timeTemp;

    private void OnEnable()
    {
        scoreText.text = $"점수 : {Player.score - scoreTemp}점";
        stageIndexText.text = $"{GameManager.stageIndex + 1}단계";
        timeText.text = $"{(int)((PlayerUI.curTime - timeTemp) / 60)}분 {(int)((PlayerUI.curTime - timeTemp) % 60)}초";

        scoreTemp = Player.score;
        timeTemp = (int)PlayerUI.curTime;
    }
}
