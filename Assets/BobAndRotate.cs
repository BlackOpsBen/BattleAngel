using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAndRotate : MonoBehaviour
{
    [SerializeField] private float bobFrequency = 3.0f;
    [SerializeField] private float bobAmplitude = 0.5f;
    [SerializeField] private float rotateSpeed = 180.0f;

    private float initialYOffset;

    private void Start()
    {
        initialYOffset = transform.localPosition.y;
    }

    private void Update()
    {
        Bob();

        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
    }

    private void Bob()
    {
        float bob = Mathf.Sin(Time.time * bobFrequency) * bobAmplitude;
        transform.localPosition = new Vector3(0.0f, initialYOffset + bob, 0.0f);
    }
}
