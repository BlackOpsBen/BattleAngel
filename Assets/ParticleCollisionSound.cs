using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleCollisionSound : MonoBehaviour
{
    ParticleSystem ps;
    [SerializeField] float interval = .1f;
    private float timer = 0f;

    int currentNumParticles = 0;

    private void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer < 0f)
        {
            if (ps.particleCount != currentNumParticles)
            {
                //AudioManager.Instance.PlayGoreAtPosition(transform.position); // TODO play sound
                timer = interval;
            }
        }

        currentNumParticles = ps.particleCount;
    }
}
