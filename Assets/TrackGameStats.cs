using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TrackGameStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeText;
    [SerializeField] private TextMeshProUGUI deathsText;

    // Update is called once per frame
    void Update()
    {
        timeText.text = GetTimeText();
    }

    private string GetTimeText()
    {
        float time = Time.time;
        int minutes = (int)time / 60;
        int seconds = (int)time - 60 * minutes;
        float fractionalSecond = time - minutes * 60 - seconds;
        int decaseconds = (int)(fractionalSecond * 100);
        Debug.Log(Time.time);
        return string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, decaseconds);
    }

    public void SetDeathsText(int numDeaths)
    {
        deathsText.text = numDeaths.ToString();
    }
}
