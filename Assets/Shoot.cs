using MilkShake;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    [SerializeField] private int shotsPerSecond = 10;
    [SerializeField] private ShakePreset shakePreset;
    [SerializeField] private Transform muzzle;
    [SerializeField] private PlayAllSubPFX muzzlePFX;
    [SerializeField] private float maxRange = 100.0f;

    private float shotTimer = 0.0f;

    private bool isFiring = false;

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
            shotTimer = 0.0f;
        }
    }

    private void PerformShot()
    {
        AudioManager.Instance.PlaySound("SC_Gun", transform);

        muzzlePFX.PlayAll();

        Shaker.ShakeAll(shakePreset);

        RaycastHit hit;

        if (Physics.Raycast(muzzle.position, muzzle.forward, out hit, maxRange))
        {
            ProcessHit(hit);
        }

        Debug.DrawRay(muzzle.position, muzzle.forward * maxRange, Color.red, 0.1f, true);
    }

    private static void ProcessHit(RaycastHit hit)
    {
        Debug.Log("Hit " + hit.transform.name);

        HitFX hitFx;

        if (hitFx = hit.transform.GetComponent<HitFX>())
        {
            Vector3 flatHitNormal = new Vector3(hit.normal.x, 0.0f, hit.normal.z);
            hitFx.Play(hit.point, Quaternion.LookRotation(flatHitNormal, Vector3.up));
        }
    }
}
