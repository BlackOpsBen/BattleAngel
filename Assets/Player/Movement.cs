using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 10f;

    private Rigidbody rb;

    //private CharacterController characterController;

    private Animator animator;

    private Vector2 movementRawInput;
    private Vector3 moveDirection;
    private Vector3 relativeForward;
    private Vector3 relativeRight;
    private Vector3 lerpMove;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        //characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
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
        Animate();
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

        transform.position += relativeMoveDirection * Time.deltaTime * moveSpeed;

        //characterController.Move(relativeMoveDirection * Time.deltaTime * moveSpeed);
    }

    private void Animate()
    {
        Vector3 relMovDir = transform.InverseTransformDirection(relativeForward * lerpMove.z + relativeRight * lerpMove.x);

        animator.SetFloat("x", relMovDir.x);
        animator.SetFloat("y", relMovDir.z);
    }

    public bool GetIsMoving()
    {
        return movementRawInput.sqrMagnitude > 0.1f;
    }
}
