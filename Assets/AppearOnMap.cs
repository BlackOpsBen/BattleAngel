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

        Vector3 direction = GetDirection();

        return direction * (mapSize/2) * percent;
    }

    private Vector3 GetDirection()
    {
        Vector3 yFlatPos = new Vector3(transform.position.x, Camera.main.transform.position.y, transform.position.z);
        Vector3 toAdd = Camera.main.transform.position - GameManager.Instance.GetPlayerInstance().transform.position;
        Vector3 correctedPos = yFlatPos + toAdd;


        Vector3 localOffset = Camera.main.transform.InverseTransformPoint(correctedPos);
        Vector3 correctedLocalOffset = new Vector3(localOffset.x, localOffset.z, 0.0f);
        correctedLocalOffset.Normalize();
        Debug.Log("Offset: " + correctedLocalOffset);
        return correctedLocalOffset;
    }
}
