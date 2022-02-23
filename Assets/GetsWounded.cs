using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetsWounded : MonoBehaviour
{
    [SerializeField] private List<LimbWoundHealth> limbWoundHealths = new List<LimbWoundHealth>();

    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void GetWounded()
    {
        animator.SetTrigger("Wounded");
    }

    public void OnRecoveredEvent()
    {
        Debug.LogWarning("Recovered!");
        foreach (LimbWoundHealth limb in limbWoundHealths)
        {
            limb.ResetHP();
        }
    }

    public void OnCryStartEvent()
    {

    }

    public void OnCryEndEvent()
    {

    }
}
