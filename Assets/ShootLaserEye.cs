using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootLaserEye : MonoBehaviour, IToggleWhenRevealed
{
    [SerializeField] private float fireDuration = 5.0f;
    [SerializeField] private float restDuration = 7.0f;
    private float timer = 0.0f;

    private bool isFiring = false;

    public void ToggleActive(bool active)
    {
        
    }

    private void Update()
    {
        timer += Time.deltaTime;
    }
}
