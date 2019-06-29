using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{
    public static LevelTimer instance;
    public float timeInLevel;
    public TMP_Text timeText;
    public List<TimeBonus> timeBonuses;
    public bool recording;
    public bool win = false;

    private void Awake()
    {
        timeText = GetComponent<TMP_Text>();
        instance = this;
        StartTimer();
    }

    private void StartTimer()
    {
        timeInLevel = 0;
        recording = true;
    }

    void Update()
    {
        if (recording)
        {
            timeInLevel += Time.deltaTime;
            int intTime = Mathf.RoundToInt(timeInLevel);
            string timeTxt = "Time : " + intTime;
            timeText.text = timeTxt;
        }
    }

    public float ReturnTimeBonus()
    {
        float timeBonus = 1f;
        recording = false;
        for (int i = 0;  i < timeBonuses.Count; i++)
        {
            if (timeInLevel < timeBonuses[i].maxTime)
            {
                timeBonus = timeBonuses[i].multiplier;
                break;
            }
        }
        if (!win)
        {
            return 1f;
        }
        else
        {
            return timeBonus;
        }
    }
}
