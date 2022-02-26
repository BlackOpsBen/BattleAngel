using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScaleFadeSeconds : MonoBehaviour
{
    [SerializeField] private float fontScale = 0.5f;

    private TextMeshProUGUI seconds;
    private float fadeTime = 1.0f;
    private float startSize;
    private float sizeDifference;

    private void Awake()
    {
        seconds = GetComponent<TextMeshProUGUI>();
        startSize = seconds.fontSize;
        sizeDifference = startSize * (1 - fontScale);
    }

    private void Update()
    {
        float value = seconds.fontSize - (Time.deltaTime * sizeDifference * fadeTime);
        seconds.fontSize = Mathf.Clamp(value, startSize * fontScale, startSize);
        
        //seconds.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), Time.deltaTime * fadeTime);
    }

    public void SetDigit(int digit)
    {
        seconds.text = digit.ToString();
        seconds.fontSize = startSize;
        seconds.color = Color.white;
    }
}
