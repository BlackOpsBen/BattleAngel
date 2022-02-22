using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHurtBehavior : MonoBehaviour, IHurt
{
    [SerializeField] private ParticleSystem bloodPFX;
    [SerializeField] private string hurtDialogName;

    public void Hurt()
    {
        bloodPFX.Play();
        AudioManager.Instance.PlayDialog("player", hurtDialogName, INTERRUPT_MODE: AudioManager.INTERRUPT_OVERLAP);
    }
}
