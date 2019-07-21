using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configuration : MonoBehaviour
{
    public static Configuration Data;

    [Header("Testing")]
    public int StartLevel = 0;


    [Header("Sentity")]
    public Sentity SentityPrefab;

    public VariablesConfig Config;


    [Header("Levels")]
    public LevelConfig[] Levels;



    private void Awake()
    {
        Data = this;
    }

    public LevelConfig CurrentLevel()
    {
        return Levels[GameFileManager.GameFile.CurrentLevel];
    }
}
