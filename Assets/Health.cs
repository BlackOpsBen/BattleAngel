using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int maxHP = 100;
    private int currentHP;
    private bool invincible = false;

    private IDie deathBehavior;

    private void Start()
    {
        currentHP = maxHP;
        deathBehavior = GetComponent<IDie>();
    }

    public void Damage(int damage)
    {
        if (!invincible)
        {
            currentHP -= damage;
            if (currentHP <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        deathBehavior.Die();
    }

    public void SetInvincible(bool value)
    {
        invincible = value;
    }
}
