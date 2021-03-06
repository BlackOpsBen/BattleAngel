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

    private bool isAttacking = false;

    private bool isActive = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        SetIsAttacking();
        FacePlayer();
    }

    private void SetIsAttacking()
    {
        float distance = Vector3.Distance(transform.position, GameManager.Instance.GetPlayerInstance().transform.position);

        isAttacking = distance < attackDistance;

        animator.SetBool("isAttacking", isAttacking && isActive);
    }

    private void FacePlayer()
    {
        if (isAttacking && isActive)
        {
            Quaternion lookRotation = Quaternion.LookRotation(GameManager.Instance.GetPlayerInstance().transform.position - transform.position);
            Quaternion lerpedLook = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * aimSpeed);
            transform.rotation = lerpedLook;
        }
    }

    public void OnGurgleEvent()
    {
        AudioManager.Instance.PlaySound("SC_Gurgle");
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
