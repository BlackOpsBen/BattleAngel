using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealthType
{
    [SerializeField] private int maxHP = 100;
    [SerializeField] private bool isPlayer = false;

    private int currentHP;
    private bool invincible = false;

    private IDie deathBehavior;
    private IHurt hurtBehavior;

    private bool isDead = false;

    private void Start()
    {
        ResetHP();
        hurtBehavior = GetComponent<IHurt>();
        deathBehavior = GetComponent<IDie>();
    }

    private void ResetHP()
    {
        currentHP = maxHP;
        UpdateHUD();
    }

    private void UpdateHUD()
    {
        if (isPlayer)
        {
            GameManager.Instance.GetHealthCounter().SetCounter(currentHP, maxHP);
        }
    }

    public void Damage(int damage)
    {
        if (!invincible && !isDead)
        {
            currentHP -= damage;

            UpdateHUD();

            if (hurtBehavior != null)
            {
                hurtBehavior.Hurt();
            }

            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        isDead = true;
        deathBehavior.Die();
    }

    public void SetInvincible(bool value)
    {
        invincible = value;
    }

    public bool GetIsDead()
    {
        return isDead;
    }
}
