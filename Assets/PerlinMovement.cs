using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMovement : MonoBehaviour
{
    [SerializeField] private float frequency = 1.0f;
    [SerializeField] private float seed = 1.0f;
    [SerializeField] private float amplitude = 1.0f;
    private void Update()
    {
        float perlinX = Mathf.PerlinNoise(seed, Time.time * frequency);
        float perlinY = Mathf.PerlinNoise(seed + 1, Time.time * frequency);
        float perlinZ = Mathf.PerlinNoise(seed + 2, Time.time * frequency);

        float perlinXPlusMinus = (perlinX * 2) - 1;
        float perlinYPlusMinus = (perlinY * 2) - 1;
        float perlinZPlusMinus = (perlinZ * 2) - 1;

        Vector3 movement = new Vector3(perlinXPlusMinus, perlinYPlusMinus, perlinZPlusMinus);

        transform.position += movement * Time.deltaTime * amplitude;
    }
}
