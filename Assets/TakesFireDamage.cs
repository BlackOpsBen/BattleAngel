using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakesFireDamage : MonoBehaviour
{
    [SerializeField] private GameObject firePFX;
    [SerializeField] private TargetParentHealth targetParentHealth;

    private bool isOnFire = false;

    private float damageInterval = 0.1f;
    private int damageAmount = 1;

    private float timer = 0.0f;

    private void Awake()
    {
        firePFX.SetActive(isOnFire);
    }

    public void SetOnFire(bool value)
    {
        firePFX.SetActive(value);
        isOnFire = value;
        timer = 0.0f;
    }

    private void Update()
    {
        timer += Time.deltaTime;

        if (isOnFire && timer > damageInterval)
        {
            targetParentHealth.GetParentHealth().Damage(damageAmount);
            timer = 0.0f;
        }
    }
}
