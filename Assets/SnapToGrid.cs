using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapToGrid : MonoBehaviour
{
    [SerializeField] private Vector3 gridSize = new Vector3(2.0f, 2.0f, 2.0f);
    [SerializeField] private float scaleIncrement = 0.5f;
    [SerializeField] private float rotationIncrement = 45f;

    private void OnDrawGizmos()
    {
        SnapPos2();
        SnapScale();
        SnapRotation();
    }

    private void SnapPos()
    {
        Vector3 position = new Vector3(
            Mathf.RoundToInt(transform.position.x),
            Mathf.RoundToInt(transform.position.y),
            Mathf.RoundToInt(transform.position.y)
        );

        this.transform.position = position;
    }

    private void SnapPos2()
    {
        Vector3 position = new Vector3(
            Mathf.Round(transform.position.x / gridSize.x) * gridSize.x,
            Mathf.Round(transform.position.y / gridSize.y) * gridSize.y,
            Mathf.Round(transform.position.z / gridSize.z) * gridSize.z
        );

        transform.position = position;
    }

    private void SnapScale()
    {
        Vector3 scale = new Vector3(
            Mathf.Round(transform.localScale.x / scaleIncrement) * scaleIncrement,
            Mathf.Round(transform.localScale.y / scaleIncrement) * scaleIncrement,
            Mathf.Round(transform.localScale.z / scaleIncrement) * scaleIncrement
        );

        transform.localScale = scale;
    }

    private void SnapRotation()
    {
        Vector3 rotation = new Vector3(
            Mathf.Round(transform.rotation.eulerAngles.x / rotationIncrement) * rotationIncrement,
            Mathf.Round(transform.rotation.eulerAngles.y / rotationIncrement) * rotationIncrement,
            Mathf.Round(transform.rotation.eulerAngles.z / rotationIncrement) * rotationIncrement
        );

        transform.rotation = Quaternion.Euler(rotation);
    }
}
