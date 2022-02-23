using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class EyeStompAttack : MonoBehaviour, IToggleWhenRevealed
{
    [SerializeField] private float attackDistance = 3.0f;
    [SerializeField] private PlayAllSubPFX leftStomp;
    [SerializeField] private PlayAllSubPFX rightStomp;
    [SerializeField] private float damageRadius = 5.0f;
    [SerializeField] private int damageAmount = 10;
    [SerializeField] private ShakePreset shakePreset;

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
        SetIsAttacking(); // TODO refactor so Enemy Attacks share same script
    }

    private void SetIsAttacking()
    {
        float distance = Vector3.Distance(transform.position, movement.GetTargetPlayer().position);

        isAttacking = distance < attackDistance;

        animator.SetBool("isAttacking", isAttacking && isActive);
    }

    public void OnStompLeftEvent()
    {
        leftStomp.PlayAll();

        if (GetPlayerInRange(leftStomp.transform.position))
        {
            movement.GetTargetPlayer().GetComponent<Health>().Damage(damageAmount);
        }

        AudioManager.Instance.PlaySound("SC_Stomp");
        Shaker.ShakeAll(shakePreset);
    }

    public void OnStompRightEvent()
    {
        rightStomp.PlayAll();

        if (GetPlayerInRange(rightStomp.transform.position))
        {
            movement.GetTargetPlayer().GetComponent<Health>().Damage(damageAmount);
        }

        AudioManager.Instance.PlaySound("SC_Stomp");
        Shaker.ShakeAll(shakePreset);
    }

    private bool GetPlayerInRange(Vector3 fromPosition)
    {
        Vector3 playerPos = movement.GetTargetPlayer().position;
        float distance = Vector3.Distance(fromPosition, playerPos);
        return distance < damageRadius;
    }

    public void ToggleActive(bool active)
    {
        isActive = active;
    }
}
