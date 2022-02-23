using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserEye : MonoBehaviour, IToggleWhenRevealed
{
    [SerializeField] private float fireDuration = 5.0f;
    [SerializeField] private float restDuration = 7.0f;
    [SerializeField] private ParticleSystem laserPFX;
    private float timer = float.MaxValue;

    private bool isFiring = false;
    private bool isActive = false;

    public void ToggleActive(bool active)
    {
        isActive = active;
    }

    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            CheckFiringConditions();
            if (timer > restDuration + fireDuration)
            {
                timer = 0.0f;
            }
        }
    }

    private void CheckFiringConditions()
    {
        if (timer > restDuration && timer < restDuration + fireDuration)
        {
            if (!isFiring)
            {
                SetFiring(true);
            }
        }
        else
        {
            if (isFiring)
            {
                SetFiring(false);
            }
        }
    }

    private void SetFiring(bool value)
    {
        isFiring = value;
        if (value)
        {
            laserPFX.Play();
        }
        else
        {
            laserPFX.Stop();
        }

        Debug.Log("Eye firing = " + isFiring);
    }
}
