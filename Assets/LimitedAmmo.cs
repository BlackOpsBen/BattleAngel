using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedAmmo : MonoBehaviour
{
    [Header("Ammo")]
    [SerializeField] private int ammoPerMag = 200;
    private int currentAmmo;

    private void Start()
    {
        RefillAmmo();
    }

    public void RefillAmmo()
    {
        currentAmmo = ammoPerMag;
        UpdateHud();

        // TODO play pickup dialog
    }

    public bool GetHasAmmo()
    {
        return currentAmmo > 0;
    }

    public void UseAmmo()
    {
        currentAmmo--;
        UpdateHud();
    }

    private void UpdateHud()
    {
        GameManager.Instance.GetAmmoCounter().SetCounter(currentAmmo, ammoPerMag);
    }
}
