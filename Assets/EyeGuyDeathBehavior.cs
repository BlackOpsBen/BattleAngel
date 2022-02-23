using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGuyDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private GameObject eyePrefab;
    [SerializeField] private Transform eyeSpawnTransform;

    public override void Die()
    {
        Instantiate(eyePrefab, eyeSpawnTransform.position, Quaternion.identity);

        base.Die();
    }
}
