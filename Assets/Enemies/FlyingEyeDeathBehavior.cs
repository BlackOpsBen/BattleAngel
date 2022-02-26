using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class FlyingEyeDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private PlayAllSubPFX deathPFX;
    [SerializeField] private ShakePreset shakePreset;
    [SerializeField] private ParticleSystem pfxToDisable;

    public override void Die()
    {
        pfxToDisable.Stop();

        deathPFX.PlayAll();

        Shaker.ShakeAll(shakePreset);

        base.Die();
    }
}
