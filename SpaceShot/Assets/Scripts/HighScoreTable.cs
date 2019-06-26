using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreTable : MonoBehaviour
{
    public Text Text;
    // Start is called before the first frame update
    void Start()
    {
        GameFileManager.Load();
        Text.text = "High Score : " + GameFileManager.GameFile.HighScore;

        GameFileManager.GameFile.CurrentScore = 0;
        GameFileManager.GameFile.CurrentLevel = 1;
        GameFileManager.Save();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
