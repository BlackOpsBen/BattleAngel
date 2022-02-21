using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayAllSubPFX : MonoBehaviour
{
    [SerializeField] private List<ParticleSystem> particleSystems = new List<ParticleSystem>();

    public void PlayAll()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Play();
        }
    }
}
