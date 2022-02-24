using MilkShake;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private int shotsPerSecond = 10;
    [SerializeField] private int damagePerShot = 10;
    [SerializeField] private ShakePreset shakePreset;
    [SerializeField] private Transform muzzle;
    [SerializeField] private PlayAllSubPFX muzzlePFX;
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private GameObject impactPFX;
    [SerializeField] private float maxRange = 100.0f;
    [SerializeField] private Vector3 spread = new Vector3(1f, 1f, 1f);
    [SerializeField] private LayerMask layerMask;

    private float shotTimer = float.MaxValue;

    private bool isFiring = false;

    private AimAssist aimAssist;

    private void Awake()
    {
        aimAssist = GetComponent<AimAssist>();
    }

    public void OnShoot(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isFiring = true;
        }

        if (context.canceled)
        {
            isFiring = false;
        }
    }

    private void Update()
    {
        if (isFiring)
        {
            shotTimer += Time.deltaTime;

            float shotInterval = 1.0f / shotsPerSecond;

            if (shotTimer > shotInterval)
            {
                shotTimer = 0.0f;

                PerformShot();
            }
        }
        else
        {
            shotTimer = float.MaxValue;
        }
    }

    private void PerformShot()
    {
        AudioManager.Instance.PlaySound("SC_Gun", transform);

        muzzlePFX.PlayAll();

        Shaker.ShakeAll(shakePreset);

        RaycastHit hit;
        Vector3 direction = GetShotDirection();
        Vector3 destination = muzzle.position + direction * maxRange;

        if (Physics.Raycast(muzzle.position, direction, out hit, maxRange, layerMask))
        {
            destination = hit.point;
            ProcessHit(hit);
        }

        TrailRenderer trail = Instantiate(bulletTrail, muzzle.position, Quaternion.identity);

        StartCoroutine(SpawnTrail(trail, destination));

        Debug.DrawRay(muzzle.position, direction * maxRange, Color.red, 0.1f, true);
    }

    private Vector3 GetShotDirection()
    {
        Vector3 direction = aimAssist.GetTarget().position - muzzle.position;

        direction.Normalize();

        direction += new Vector3(
            UnityEngine.Random.Range(-spread.x, spread.x),
            UnityEngine.Random.Range(-spread.y, spread.y),
            UnityEngine.Random.Range(-spread.z, spread.z)
        );

        direction.Normalize();

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, Vector3 destination)
    {
        float time = 0.0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, destination, time);
            time += Time.deltaTime / trail.time;

            yield return null;
        }

        trail.transform.position = destination;

        Destroy(trail.gameObject, trail.time);
    }

    private void ProcessHit(RaycastHit hit)
    {
        if (hit.collider.GetComponent<Movement>())
        {
            Debug.LogWarning("Player hit self!");
        }
        else
        {
            HitFX hitFx;

            if (hitFx = hit.transform.GetComponent<HitFX>())
            {
                Vector3 flatHitNormal = new Vector3(hit.normal.x, 0.0f, hit.normal.z);
                hitFx.Play(hit.point, Quaternion.LookRotation(flatHitNormal, Vector3.up));
            }

            DealDamage(hit);

            Instantiate(impactPFX, hit.point, Quaternion.LookRotation(hit.normal));
        }
    }

    private void DealDamage(RaycastHit hit)
    {
        IHealthType health;
        health = hit.transform.GetComponent<IHealthType>();

        if (health != null)
        {
            health.Damage(damagePerShot);
            return;
        }

        TargetParentHealth targetParentHealth;

        if (targetParentHealth = hit.transform.GetComponent<TargetParentHealth>())
        {
            health = targetParentHealth.GetParentHealth();
            health.Damage(damagePerShot);
        }
    }
}
