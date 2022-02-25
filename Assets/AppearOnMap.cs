using System;
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

    internal Vector3 GetMapPosition(float mapSize)
    {
        float sqrDistFromPlayer = (GameManager.Instance.GetPlayerInstance().transform.position - transform.position).sqrMagnitude;

        float percent = MiniMap.Instance.mapDistCurve.Evaluate(sqrDistFromPlayer / MiniMap.Instance.maxDistSqr);

        Vector3 direction = Vector3.up;

        return direction * (mapSize/2) * percent;
    }
}
