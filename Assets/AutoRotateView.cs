using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoRotateView : MonoBehaviour
{
    private Movement movement;

    [SerializeField] private float maxRotationSpeed = 2.0f;
    [SerializeField] private float accelerationTime = 2.0f;
    [SerializeField] private float deccelerationTime = 1.0f;
    private float rotationSpeedPercent = 0.0f;

    private void Awake()
    {
        movement = FindObjectOfType<Movement>();
    }

    private void Update()
    {
        UpdateRotationSpeedPercent();
        RotateTowardView();
    }

    private void UpdateRotationSpeedPercent()
    {
        if (movement.GetIsMoving())
        {
            rotationSpeedPercent += Time.deltaTime / accelerationTime;
        }
        else
        {
            rotationSpeedPercent -= Time.deltaTime / deccelerationTime;
        }

        rotationSpeedPercent = Mathf.Clamp01(rotationSpeedPercent);
    }

    private void RotateTowardView()
    {
        Quaternion facingRotation = Quaternion.LookRotation(movement.transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, facingRotation, maxRotationSpeed * rotationSpeedPercent * Time.deltaTime);
    }
}
