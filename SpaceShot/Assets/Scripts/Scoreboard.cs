using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scoreboard : MonoBehaviour
{
    public int score = 0;
    public Text scoreText;
    public static Scoreboard scoreboard;

    public void Awake()
    {
        scoreboard = this;
        UpdateScore();
    }

    public void IncreaseScore(int amount)
    {
        score += amount;
        UpdateScore();
    }

    public void UpdateScore()
    {
        scoreText.text = ("Score : " + score);
    }

    public void LogScore()
    {

        if (GameFileManager.GameFile.HighScore < score)
        {
            GameFileManager.GameFile.HighScore = score;
            GameFileManager.Save();
        }

    }
}
