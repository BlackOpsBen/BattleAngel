using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtOnTouch : MonoBehaviour
{
    [SerializeField] private int damageAmount = 5;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Movement>())
        {
            Health health = collision.gameObject.GetComponent<Health>();
            health.Damage(damageAmount);
        }
    }
}
