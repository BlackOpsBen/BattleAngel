using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 10f;

    private CharacterController characterController;

    private Vector2 movementRawInput;
    private Vector3 moveDirection;
    private Vector3 relativeForward;
    private Vector3 relativeRight;
    private Vector3 lerpMove;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
        lerpMove = Vector3.zero;
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movementRawInput = context.ReadValue<Vector2>();
    }

    private void Update()
    {
        SetRelativeDirections();
        SetMoveDirection();
        PerformMove();

        //characterController.Move(move * Time.deltaTime * moveSpeed);
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

    private void SetMoveDirection()
    {
        moveDirection = new Vector3(movementRawInput.x, 0.0f, movementRawInput.y);
    }

    private void PerformMove()
    {
        lerpMove = Vector3.Lerp(lerpMove, moveDirection, Time.deltaTime * acceleration);

        Vector3 relativeMoveDirection = relativeForward * lerpMove.z + relativeRight * lerpMove.x;

        characterController.Move(relativeMoveDirection * Time.deltaTime * moveSpeed);
    }
}
