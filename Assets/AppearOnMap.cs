using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppearOnMap : MonoBehaviour
{
    [SerializeField] private Color color = Color.red;
    [SerializeField] private float scale = 1.0f;
    private bool showOnMap = true;

    private void Start()
    {
        MiniMap.Instance.AddItemToMap(this);
    }

    public Color GetColor()
    {
        return color;
    }

    public float GetScale()
    {
        return scale;
    }

    public void SetShowOnMap(bool value)
    {
        showOnMap = value;
    }

    public bool GetShowOnMap()
    {
        return showOnMap;
    }
}
