using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour, IHealthType
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private bool invincible = false;

    private IDie deathBehavior;
    private IHurt hurtBehavior;

    private bool isDead = false;

    private void Start()
    {
        currentHP = maxHP;
        hurtBehavior = GetComponent<IHurt>();
        deathBehavior = GetComponent<IDie>();
    }

    public void Damage(int damage)
    {
        if (!invincible && !isDead)
        {
            currentHP -= damage;

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
}
