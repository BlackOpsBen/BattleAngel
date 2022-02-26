using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TagToggleManager))]
public class GetsWounded : MonoBehaviour
{
    [SerializeField] private List<LimbWoundHealth> limbWoundHealths = new List<LimbWoundHealth>();
    [SerializeField] private ParticleSystem cryPFX;
    [SerializeField] private string woundedSoundName = "SC_EuyGuy_Hurt";

    private TagToggleManager tagToggle;

    private Animator animator;

    private void Awake()
    {
        tagToggle = GetComponent<TagToggleManager>();
        animator = GetComponent<Animator>();
    }

    public void GetWounded()
    {
        animator.SetTrigger("Wounded");

        tagToggle.SetIsWounded(true);

        AudioManager.Instance.PlaySound(woundedSoundName);
    }

    public void OnRecoveredEvent()
    {
        foreach (LimbWoundHealth limb in limbWoundHealths)
        {
            limb.ResetHP();
        }

        tagToggle.SetIsWounded(false);
    }

    public void OnCryStartEvent()
    {
        cryPFX.Play();

        GameManager.Instance.GetComponent<GonnaCryDialog>().PlaySound();
    }

    public void OnCryEndEvent()
    {
        cryPFX.Stop();
    }
}
