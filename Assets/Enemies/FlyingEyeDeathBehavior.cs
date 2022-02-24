using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MilkShake;

public class FlyingEyeDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private PlayAllSubPFX deathPFX;
    [SerializeField] private ShakePreset shakePreset;

    public override void Die()
    {
        deathPFX.PlayAll();

        Shaker.ShakeAll(shakePreset);

        // TODO play eye death sound

        base.Die();
    }
}
