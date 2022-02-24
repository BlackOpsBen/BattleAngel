using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAllSubPFX : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    [SerializeField] private bool playOnStart = false;

    private void Start()
    {
        if (playOnStart)
        {
            PlayAll();
        }
    }

    public void PlayAll()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }
}
