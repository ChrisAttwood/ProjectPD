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

        //score = GameFileManager.GameFile.CurrentScore;

        //UpdateScore();
    }

    private void Start()
    {
        unmodifiedScore = GameFileManager.GameFile.CurrentScore;
        //Debug.Log(GameFileManager.GameFile.CurrentScore);
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
            GameFileManager.GameFile.CurrentScore += score;
            GameFileManager.Save();
            if (GameFileManager.GameFile.HighScore < score)
            {
                GameFileManager.GameFile.HighScore = score;
                GameFileManager.Save();
            }



        }

    }
}
