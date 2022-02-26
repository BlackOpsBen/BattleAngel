using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;
using System;

public class CineAutoRotate : MonoBehaviour
{
    [SerializeField] private float maxRotationSpeed = 2.0f;
    [SerializeField] private float accelerationTime = 2.0f;
    [SerializeField] private float deccelerationTime = 1.0f;
    private float rotationSpeedPercent = 0.0f;
    private CinemachineFreeLook freeLookComponent;

    private float debugAxis;
    public float debugSpeed = 2.0f;

    private void Start()
    {
        freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        Movement movement = GameManager.Instance.GetPlayerInstance().GetComponent<Movement>();
        UpdateRotationSpeedPercent(movement);
        //RotateTowardView(movement);
        UpdateFreeLook(movement);

        freeLookComponent.m_XAxis.Value += debugAxis * Time.deltaTime * debugSpeed;
    }

    private void UpdateRotationSpeedPercent(Movement movement)
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

    /*private void RotateTowardView(Movement movement)
    {
        Quaternion facingRotation = Quaternion.LookRotation(movement.transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, facingRotation, maxRotationSpeed * rotationSpeedPercent * Time.deltaTime);
    }*/

    private void UpdateFreeLook(Movement movement)
    {
        Vector3 fromDirection = transform.forward;
        Vector3 toDirection = movement.transform.forward;

        float signedAngle = Vector3.SignedAngle(fromDirection, toDirection, Vector3.up);

        Quaternion axisQuat = Quaternion.Euler(0.0f, freeLookComponent.m_XAxis.Value, 0.0f);
        Quaternion signedQuat = Quaternion.Euler(0.0f, signedAngle, 0.0f);
        Quaternion lerpedQuat = Quaternion.Slerp(axisQuat, signedQuat, maxRotationSpeed * rotationSpeedPercent * Time.deltaTime);

        float lerpedValue = lerpedQuat.eulerAngles.y;

        freeLookComponent.m_XAxis.Value = lerpedValue;
    }

    public void OnMoveXAxis(InputAction.CallbackContext context)
    {
        debugAxis = context.ReadValue<float>();
    }
}
