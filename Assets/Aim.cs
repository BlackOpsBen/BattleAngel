using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Aim : MonoBehaviour
{
    [SerializeField] float turnSpeed = 5.0f;
    [SerializeField] float acceleration = 5.0f;
    [SerializeField] float minAngleThreshold = 5.0f;

    private Vector2 aimRawInput;
    private Vector3 relativeForward;
    private Vector3 relativeRight;

    Vector3 lookDirection;
    Vector3 slerpLook;

    private void Start()
    {
        slerpLook = Vector3.forward;
    }

    public void OnAim(InputAction.CallbackContext context)
    {
        aimRawInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        if (aimRawInput.sqrMagnitude > 0.1f)
        {
            SetRelativeDirections();
            SetLookDirection();
            PerformAim();
        }
    }

    private void SetRelativeDirections()
    {
        relativeForward = Camera.main.transform.forward;
        relativeRight = Vector3.Cross(relativeForward, Vector3.down);

        relativeForward.y = 0.0f;
        relativeForward = relativeForward.normalized;
        relativeRight.y = 0.0f;
        relativeRight = relativeRight.normalized;
    }

    private void SetLookDirection()
    {
        lookDirection = new Vector3(aimRawInput.x, 0.0f, aimRawInput.y);
    }

    private void PerformAim()
    {
        slerpLook = Vector3.Slerp(slerpLook, lookDirection, Time.deltaTime * acceleration);

        Vector3 relativeLookDirection = relativeForward * slerpLook.z + relativeRight * slerpLook.x;

        transform.rotation = Quaternion.LookRotation(relativeLookDirection);
    }
}
