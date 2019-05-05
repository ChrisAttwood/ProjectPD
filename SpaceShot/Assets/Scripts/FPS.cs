using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class FPS : MonoBehaviour
{

    public Text Display;

    float deltaTime = 0.0f;


    void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.1f;
        float fps = 1.0f / deltaTime;
        string text = string.Format("{0:0.} FPS", fps);
        Display.text = text;
    }
}
