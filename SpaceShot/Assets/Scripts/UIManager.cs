using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    public GameObject GameOverCanvas;
    public GameObject VictoryCanvas;

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
}
