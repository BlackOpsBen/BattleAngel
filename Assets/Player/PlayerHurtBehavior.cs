using MilkShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBehavior : MonoBehaviour, IHurt
{
    [SerializeField] private ParticleSystem bloodPFX;
    [SerializeField] private string hurtDialogName;
    [SerializeField] private ShakePreset cameraShakePreset;

    public void Hurt()
    {
        bloodPFX.Play();
        AudioManager.Instance.PlayDialog(AudioManager.PLAYERNAME, hurtDialogName, INTERRUPT_MODE: AudioManager.INTERRUPT_OVERLAP);
        Shaker.ShakeAll(cameraShakePreset);
    }
}
