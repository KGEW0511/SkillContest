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
        scoreText.text = $"���� : {Player.score - scoreTemp}��";
        stageIndexText.text = $"{GameManager.stageIndex + 1}�ܰ�";
        timeText.text = $"{(int)((PlayerUI.curTime - timeTemp) / 60)}�� {(int)((PlayerUI.curTime - timeTemp) % 60)}��";

        scoreTemp = Player.score;
        timeTemp = (int)PlayerUI.curTime;
    }
}
