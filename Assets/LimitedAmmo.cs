using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedAmmo : MonoBehaviour
{
    [SerializeField] private int ammoPerMag = 200;
    private int currentAmmo;

    private void Awake()
    {
        RefillAmmo();
    }

    private void RefillAmmo()
    {
        currentAmmo = ammoPerMag;
    }

    public bool GetHasAmmo()
    {
        return currentAmmo > 0;
    }
}
