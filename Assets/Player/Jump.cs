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
    public float apexHeight = 0.0f;
    public float landHeight = 0.0f;

    public bool isInAir = false;
    public bool newState = false;

    public bool isJumping = false;
    public bool hasJumped = false;

    private void Update()
    {
        if (hasJumped && GetIsTouchingGround())
        {
            animator.SetTrigger("Land");
            hasJumped = false;
        }
        
        //OldJump();

        UpdateApex();
    }

    private void OldJump()
    {
        newState = !GetIsTouchingGround();
        if (!newState && isInAir)
        {
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
    }

    private void UpdateApex()
    {
        if (transform.position.y > apexHeight)
        {
            apexHeight = transform.position.y;
        }
    }

    // Called by input to ATTEMPT jump
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!isJumping && GetIsTouchingGround())
            {
                StartJumpAnimation();
            }
            /*if (!isInAir)
            {
                StartJumpAnimation();
            }*/
        }
    }

    // Called by animation event
    public void OnJumpEvent()
    {
        PerformJump();
    }

    public void OnHasJumpedEvent()
    {
        isJumping = false;
        hasJumped = true;
    }

    private void StartJumpAnimation()
    {
        animator.SetTrigger("Jump");
        isJumping = true;
    }

    private void PerformJump()
    {
        rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);

        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, "SC_Jump", INTERRUPT_MODE: AudioManager.INTERRUPT_OVERLAP);
    }

    private bool GetIsTouchingGround()
    {
        return Physics.CheckSphere(groundChecker.position, groundCheckRadius, layerMask);
    }
}
