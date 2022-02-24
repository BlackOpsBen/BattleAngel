using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMovement : MonoBehaviour
{
    [SerializeField] private float frequency = 1.0f;
    [SerializeField] private float seed = 1.0f;
    [SerializeField] private float amplitude = 1.0f;
    [SerializeField] private float ySnapStrength = 1.0f;
    [SerializeField] private float ySnapHeight = 3.5f;
    private void Update()
    {
        Debug.Log("Actual Y: " + transform.position.y);
        Debug.Log("Distance to desired Y: " + (transform.position.y - ySnapHeight));

        float perlinX = Mathf.PerlinNoise(seed, Time.time * frequency);
        float perlinY = Mathf.PerlinNoise(seed + 1, Time.time * frequency);
        float perlinZ = Mathf.PerlinNoise(seed + 2, Time.time * frequency);

        float perlinXPlusMinus = (perlinX * 2) - 1;
        float perlinYPlusMinus = (perlinY * 2) - 1;
        float perlinZPlusMinus = (perlinZ * 2) - 1;

        float lerpYPos = Mathf.Lerp(transform.position.y, ySnapHeight, Time.deltaTime * ySnapStrength);

        Vector3 perlinOffset = new Vector3(perlinXPlusMinus, perlinYPlusMinus, perlinZPlusMinus);
        Vector3 ySnappedPos = new Vector3(transform.position.x, lerpYPos, transform.position.z);
        Vector3 newPos = ySnappedPos + (perlinOffset * Time.deltaTime * amplitude);

        transform.position = newPos;
    }
}
