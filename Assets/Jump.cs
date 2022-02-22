using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Jump : MonoBehaviour
{
    [SerializeField] private Animator animator;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float jumpStrength = 10.0f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private float groundCheckRadius = 0.26f;
    [SerializeField] private LayerMask layerMask;

    [SerializeField] private GroundPound groundPound;
    private float apexHeight = 0.0f;
    private float landHeight = 0.0f;

    public bool isInAir = false;
    public bool newState = false;

    private void Update()
    {
        newState = !GetIsTouchingGround();
        if (!newState && isInAir)
        {
            Debug.Log("Actually landed");

            // Record landing height
            landHeight = transform.position.y;

            animator.SetTrigger("Land");
            if (groundPound != null)
            {
                groundPound.CheckJump(apexHeight, landHeight);
            }

            // Reset Apex height
            apexHeight = float.MinValue;
        }
        isInAir = newState;

        UpdateApex();
    }

    private void UpdateApex()
    {
        if (transform.position.y > apexHeight)
        {
            apexHeight = transform.position.y;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isInAir)
            {
                StartJumpAnimation();
            }
        }
    }

    public void OnJumpEvent()
    {
        PerformJump();
    }

    private void StartJumpAnimation()
    {
        animator.SetTrigger("Jump");
    }

    private void PerformJump()
    {
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
    }

    private bool GetIsTouchingGround()
    {
        return Physics.CheckSphere(groundChecker.position, groundCheckRadius, layerMask);
    }
}
