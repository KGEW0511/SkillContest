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
        scoreText.text = string.Format("���� : {0}", Player.score);
        stageText.text = string.Format("�������� : {0}", GameManager.stageIndex);
        timeText.text = string.Format("�ð� : {0}�� {1}��", (int)PlayerUI.time / 60, (int)PlayerUI.time % 60);
    }
}
