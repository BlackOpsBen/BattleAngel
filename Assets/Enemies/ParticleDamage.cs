using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnParticleCollision(GameObject other)
    {
        Debug.Log("particle hit " + other.name);
        Health health;
        if (health = other.GetComponent<Health>())
        {
            health.Damage(damageAmount);
        }
    }
}
