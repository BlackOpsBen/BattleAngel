using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParentHealth : MonoBehaviour
{
    [SerializeField] private MonoBehaviour parentHealth;

    private IHealthType health;

    private void Awake()
    {
        health = (IHealthType)parentHealth;
    }

    public IHealthType GetParentHealth()
    {
        return health;
    }
}
