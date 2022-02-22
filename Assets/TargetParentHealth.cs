using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetParentHealth : MonoBehaviour
{
    [SerializeField] private Health parentHealth;

    public Health GetParentHealth()
    {
        return parentHealth;
    }
}
