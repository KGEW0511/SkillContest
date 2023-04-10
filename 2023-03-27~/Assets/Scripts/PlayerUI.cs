using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Player player;
    public Text scoreText;
    public Text fuelText;
    public Text timeText;
    public Text skill1CollTime;
    public Text skill2CollTime;
    public Slider fuelBar;
    public Image Life0;
    public Image Life1;
    public Image Life2;
    public Image Life3;
    public Image Life4;
    public Image Clock;

    public float maxFuel;
    public float maxTime;
    static public float curTime;
    private void Awake()
    {
        maxFuel = player.fuel;
        Life0.sprite = player.spriteObjs[player.spriteNumber];
        Life1.sprite = player.spriteObjs[player.spriteNumber];
        Life2.sprite = player.spriteObjs[player.spriteNumber];
        Life3.sprite = player.spriteObjs[player.spriteNumber];
        Life4.sprite = player.spriteObjs[player.spriteNumber];
    }
    private void FixedUpdate()
    {
        Score();
        Life();
        Fuel();
        ClockTime();
        SkillCoolTime();
    }

    void Life()
    {
        switch (player.life)
        {
            case 0:
                Life0.gameObject.SetActive(false);
                break;
            case 1:
                Life0.gameObject.SetActive(true);
                Life1.gameObject.SetActive(false);
                break;
            case 2:
                Life1.gameObject.SetActive(true);
                Life2.gameObject.SetActive(false);
                break;
            case 3:
                Life2.gameObject.SetActive(true);
                Life3.gameObject.SetActive(false);
                break;
            case 4:
                Life3.gameObject.SetActive(true);
                Life4.gameObject.SetActive(false);
                break;
            case 5:
                Life4.gameObject.SetActive(true);
                break;
        }
    }
    void Score()
    {
        scoreText.text = $"점수 : {Player.score}";
    }
    void Fuel()
    {
        fuelBar.value = (int)player.fuel / maxFuel;
        fuelText.text = $"{(int)player.fuel / maxFuel * 100}%";
    }
    void ClockTime()
    {
        curTime += Time.deltaTime;
        timeText.text = $"{(int)(maxTime - curTime) / 60}분 {(int)(maxTime - curTime) % 60}초";
    }
    void SkillCoolTime()
    {
        skill1CollTime.text = $"{(player.maxSkill1Delay > player.curSkill1Delay ? (int)(player.maxSkill1Delay - player.curSkill1Delay) : player.skill1Count) }";
        skill2CollTime.text = $"{(player.maxSkill2Delay > player.curSkill2Delay ? (int)(player.maxSkill2Delay - player.curSkill2Delay) : player.skill2Count) }";
    }
}
