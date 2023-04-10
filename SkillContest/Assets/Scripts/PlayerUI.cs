using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerUI : MonoBehaviour
{
    public Player player;

    public GameObject setting;

    public Slider hpBar;
    public Text hpText;

    public Slider fuelBar;
    public Text fuelText;

    public Image clock;
    public Text timeText;

    public Text scoreText;

    bool set;

    static public float time;

    void Start()
    {
        set = false;
    }
    void Update()
    {
        time += Time.deltaTime;

        hpText.text = string.Format("{0}%", (int)player.life);
        hpBar.value = player.life / 100;

        fuelText.text = string.Format("{0}%", (int)player.fuel);
        fuelBar.value = player.fuel / 100;

        timeText.text = string.Format("{0} : {1}", (int)((600 - time) / 60), (int)((600 - time) % 60));
        clock.fillAmount = (600 - time) / 600;

        scoreText.text = string.Format("Á¡¼ö : {0}", Player.score);
    }

    public void Setting()
    {
        if (!set)
        {
            set = true;
            setting.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            set = false;
            setting.SetActive(false);
            Time.timeScale = 1;
        }
    }
}
