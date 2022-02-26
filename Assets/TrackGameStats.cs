using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TrackGameStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI deathsText;

    private float totalTime;
    private int totalDeaths;

    private bool isCounting = true;

    // Update is called once per frame
    void Update()
    {
        if (isCounting)
        {
            timeText.text = GetTimeText();
        }
    }

    private string GetTimeText()
    {
        totalTime = Time.time;
        int minutes = (int)totalTime / 60;
        int seconds = (int)totalTime - 60 * minutes;
        float fractionalSecond = totalTime - minutes * 60 - seconds;
        int decaseconds = (int)(fractionalSecond * 100);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, decaseconds);
    }

    public void SetDeathsText(int numDeaths)
    {
        if (isCounting)
        {
            deathsText.text = numDeaths.ToString();
            totalDeaths = numDeaths;
        }
    }

    public void StopClock()
    {
        isCounting = false;
    }

    public float GetTotalTime()
    {
        return totalTime;
    }

    public int GetTotalDeaths()
    {
        return totalDeaths;
    }
}
