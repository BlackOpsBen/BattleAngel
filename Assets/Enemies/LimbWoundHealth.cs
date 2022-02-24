using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimbWoundHealth : MonoBehaviour, IHealthType
{
    [SerializeField] private GetsWounded getsWounded;

    [SerializeField] private int HP = 100;
    private int currentHP;

    private bool isWounded = false;

    private void Awake()
    {
        ResetHP();
    }

    public void ResetHP()
    {
        currentHP = HP;
        isWounded = false;
    }

    public void Damage(int amount)
    {
        currentHP -= amount;

        if (currentHP < 0 && !isWounded)
        {
            Wounded();
        }
    }

    private void Wounded()
    {
        isWounded = true;
        getsWounded.GetWounded();
    }
}
