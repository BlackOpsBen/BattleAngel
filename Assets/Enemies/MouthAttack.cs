using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthAttack : MonoBehaviour, IToggleWhenRevealed
{
    [SerializeField] private float attackDistance = 10.0f;
    [SerializeField] private float aimSpeed = 1.0f;
    [SerializeField] private ParticleSystem spitPFX;

    private Animator animator;

    private NavMeshMovement movement;

    private bool isAttacking = false;

    private bool isActive = true;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        movement = GetComponent<NavMeshMovement>();
    }

    private void Update()
    {
        SetIsAttacking();
        FacePlayer();
    }

    private void SetIsAttacking()
    {
        float distance = Vector3.Distance(transform.position, movement.GetTargetPlayer().position);

        isAttacking = distance < attackDistance;

        animator.SetBool("isAttacking", isAttacking && isActive);
    }

    private void FacePlayer()
    {
        if (isAttacking && isActive)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement.GetTargetPlayer().position - transform.position);
            Quaternion lerpedLook = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * aimSpeed);
            transform.rotation = lerpedLook;
        }
    }

    public void OnSpitEvent()
    {
        spitPFX.Play();
    }

    public void ToggleActive(bool active)
    {
        isActive = active;
    }
}
