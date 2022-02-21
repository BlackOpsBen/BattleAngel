using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouthAttack : MonoBehaviour
{
    [SerializeField] private float attackDistance = 10.0f;
    [SerializeField] private float aimSpeed = 1.0f;

    private Animator animator;

    private NavMeshMovement movement;

    private bool isAttacking = false;

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

        if (distance < attackDistance)
        {
            isAttacking = true;
        }

        animator.SetBool("isAttacking", isAttacking);
    }

    private void FacePlayer()
    {
        if (isAttacking)
        {
            Quaternion lookRotation = Quaternion.LookRotation(movement.GetTargetPlayer().position - transform.position);
            Quaternion lerpedLook = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * aimSpeed);
            transform.rotation = lerpedLook;
        }
    }

    public void OnSpitEvent()
    {
        Debug.Log("Spit!");
    }
}
