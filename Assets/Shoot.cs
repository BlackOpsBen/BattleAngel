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
    [SerializeField] private TrailRenderer bulletTrail;
    [SerializeField] private GameObject impactPFX;
    [SerializeField] private float maxRange = 100.0f;
    [SerializeField] private Vector3 spread = new Vector3(1f, 1f, 1f);

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
        Vector3 direction = GetShotDirection();

        if (Physics.Raycast(muzzle.position, direction, out hit, maxRange))
        {
            TrailRenderer trail = Instantiate(bulletTrail, muzzle.position, Quaternion.identity);

            StartCoroutine(SpawnTrail(trail, hit));

            ProcessHit(hit);
        }

        Debug.DrawRay(muzzle.position, direction * maxRange, Color.red, 0.1f, true);
    }

    private Vector3 GetShotDirection()
    {
        Vector3 direction = muzzle.forward;

        direction += new Vector3(
            UnityEngine.Random.Range(-spread.x, spread.x),
            UnityEngine.Random.Range(-spread.y, spread.y),
            UnityEngine.Random.Range(-spread.z, spread.z)
        );

        direction.Normalize();

        return direction;
    }

    private IEnumerator SpawnTrail(TrailRenderer trail, RaycastHit hit)
    {
        float time = 0.0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1)
        {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime;

            yield return null;
        }

        trail.transform.position = hit.point;

        Instantiate(impactPFX, hit.point, Quaternion.LookRotation(hit.normal));

        Destroy(trail.gameObject, trail.time);
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
