using MilkShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GirlDeathBehavior : DefaultDeathBehavior
{
    [SerializeField] private List<MonoBehaviour> scriptsToDisable = new List<MonoBehaviour>();
    [SerializeField] private Rigidbody rigidbodyToDisable;
    [SerializeField] private ShakePreset shakePreset;

    public override void Die()
    {
        foreach (MonoBehaviour script in scriptsToDisable)
        {
            script.enabled = false;
        }

        Destroy(rigidbodyToDisable);

        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, "SC_PlayerDeath", INTERRUPT_MODE: AudioManager.INTERRUPT_SELF);

        Shaker.ShakeAll(shakePreset);

        base.Die();
    }
}
