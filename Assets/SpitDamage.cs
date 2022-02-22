using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpitDamage : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;

    private void OnParticleCollision(GameObject other)
    {
        Health health;
        if (health = other.GetComponent<Health>())
        {
            health.Damage(damageAmount);
        }
    }
}
