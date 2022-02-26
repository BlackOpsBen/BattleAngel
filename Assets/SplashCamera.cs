using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashCamera : MonoBehaviour
{
    [SerializeField] private float roationSpeed = 10.0f;

    private void Update()
    {
        transform.Rotate(Vector3.up, roationSpeed * Time.deltaTime);
    }
}
