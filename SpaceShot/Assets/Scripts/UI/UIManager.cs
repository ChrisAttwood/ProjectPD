using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject GameOverCanvas;
    public GameObject VictoryCanvas;
    public GameObject LevelCompleteCanvas;

    private void Awake()
    {
        instance = this;
    }

  
    public void GameOver()
    {
        GameOverCanvas.SetActive(true);
    }

    public void Victory()
    {
        GameOverCanvas.SetActive(false);
        VictoryCanvas.SetActive(true);
    }

    public void LevelComplete()
    {
        GameOverCanvas.SetActive(false);
        LevelCompleteCanvas.SetActive(true);
    }
}
