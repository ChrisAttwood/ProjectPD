using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunScore : MonoBehaviour
{
    public static RunScore instance; 
    public Text CurrentBestText;
    public Text RunScoreText;
    public Text TimeMultiplierText;
    public Text FinalScoreText;

    private void Awake()
    {
        instance = this;
    }



    public void Display()
    {
        CurrentBestText.text = "Best : " + GameFileManager.GameFile.HighScore;
        RunScoreText.text = "This Run : " + Scoreboard.scoreboard.unmodifiedScore;
        TimeMultiplierText.text = "Time Bonus x " +LevelTimer.instance.ReturnTimeBonus();
        FinalScoreText.text = "Final Score : " + Scoreboard.scoreboard.score;
    }
}
