using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeGuyDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private GameObject eyePrefab;
    [SerializeField] private Transform eyeSpawnTransform;
    [SerializeField] private ParticleSystem cryPFXtoStop;

    public override void Die()
    {
        Instantiate(eyePrefab, eyeSpawnTransform.position, Quaternion.identity);

        cryPFXtoStop.Stop();

        base.Die();
    }
}
