﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class LevelDisplay : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // GetComponent<Text>().text = "Level: " + GameFileManager.GameFile.CurrentLevel;
        GetComponent<Text>().text = Configuration.Data.CurrentLevel().Name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
