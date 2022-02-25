using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CineAutoRotate : MonoBehaviour
{
    [SerializeField] private float maxRotationSpeed = 2.0f;
    [SerializeField] private float accelerationTime = 2.0f;
    [SerializeField] private float deccelerationTime = 1.0f;
    private float rotationSpeedPercent = 0.0f;
    private CinemachineFreeLook freeLookComponent;

    private void Start()
    {
        freeLookComponent = GetComponent<CinemachineFreeLook>();
    }

    private void Update()
    {
        Movement movement = GameManager.Instance.GetPlayerInstance().GetComponent<Movement>();
        UpdateRotationSpeedPercent(movement);
        //RotateTowardView(movement);
        UpdateFreeLook();
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

    private void RotateTowardView(Movement movement)
    {
        Quaternion facingRotation = Quaternion.LookRotation(movement.transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, facingRotation, maxRotationSpeed * rotationSpeedPercent * Time.deltaTime);
    }

    private void UpdateFreeLook()
    {

    }
}
