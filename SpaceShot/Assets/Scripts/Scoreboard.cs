using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    public int unmodifiedScore = 0;
    public float timeBonus = 0f;
    public int score = 0;
    public TMP_Text scoreText;

    public static Scoreboard scoreboard;

    public HudText CashPrefab;
    

    public void Awake()
    {
        scoreboard = this;
        UpdateScore();
    }

    public void IncreaseScore(int amount,Vector2 pos)
    {
        var cash = Instantiate(CashPrefab);
        cash.Set(pos, amount.ToString());

        unmodifiedScore += amount;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = ("Score : " + unmodifiedScore);
    }

    public void LogScore()
    {
        timeBonus = LevelTimer.instance.ReturnTimeBonus();
        score = Mathf.RoundToInt(unmodifiedScore * timeBonus);
        if(GameFileManager.GameFile!=null)
        {
            if (GameFileManager.GameFile.HighScore < score)
            {
                GameFileManager.GameFile.HighScore = score;
                GameFileManager.Save();
            }

        }

    }
}
