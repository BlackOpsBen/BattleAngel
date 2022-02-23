using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsWounded : MonoBehaviour
{
    [SerializeField] private List<LimbWoundHealth> limbWoundHealths = new List<LimbWoundHealth>();
    [SerializeField] private ParticleSystem cryPFX;

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
    }

    public void OnRecoveredEvent()
    {
        Debug.LogWarning("Recovered!");
        foreach (LimbWoundHealth limb in limbWoundHealths)
        {
            limb.ResetHP();
        }

        tagToggle.SetIsWounded(false);
    }

    public void OnCryStartEvent()
    {
        cryPFX.Play();
    }

    public void OnCryEndEvent()
    {
        cryPFX.Stop();
    }
}
