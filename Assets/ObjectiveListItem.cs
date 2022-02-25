using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveListItem : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private Image checkboxImage;
    [SerializeField] private TextMeshProUGUI text;

    [Header("Checkbox State Sprites")]
    [SerializeField] private Sprite boxChecked;
    [SerializeField] private Sprite boxUnchecked;

    [HideInInspector]
    public string name;

    public void InitializeObjective(string objectiveText, string append = "")
    {
        checkboxImage.sprite = boxUnchecked;
        text.text = objectiveText + append;
        name = objectiveText;
    }

    public void MarkComplete()
    {
        Color fadedColor = new Color(0.5f, 0.5f, 0.5f, 1.0f);
        checkboxImage.sprite = boxChecked;
        checkboxImage.color = fadedColor;
        text.color = fadedColor;
    }
}
