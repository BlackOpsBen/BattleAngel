using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudCounter : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI counter;
    //[SerializeField] private RectTransform barContainer;
    [SerializeField] private RectTransform barFill;

    public void SetCounter(int count, int outOf)
    {
        counter.text = count.ToString();
        float percent = (float)count / outOf;
        barFill.localScale = new Vector3(barFill.localScale.x, percent);
    }
}