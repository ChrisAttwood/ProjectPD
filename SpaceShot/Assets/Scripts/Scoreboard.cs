using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public int unmodifiedScore = 0;
    public float timeBonus = 0f;
    public int score = 0;
    public Text scoreText;
    public Text timeBonusText;
    public Text finalScoreText;
    public static Scoreboard scoreboard;

    public void Awake()
    {
        //GameFileManager.GameFile.HighScore = 0;
        //GameFileManager.Save();
        scoreboard = this;
        UpdateScore();
    }

    public void IncreaseScore(int amount)
    {
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

        if (GameFileManager.GameFile.HighScore < score)
        {
            GameFileManager.GameFile.HighScore = score;
            GameFileManager.Save();
        }

    }
}
