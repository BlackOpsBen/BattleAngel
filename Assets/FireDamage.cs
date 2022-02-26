using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        TakesFireDamage takesFireDamage = other.GetComponent<TakesFireDamage>();

        if (takesFireDamage != null)
        {
            takesFireDamage.SetOnFire(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        TakesFireDamage takesFireDamage = other.GetComponent<TakesFireDamage>();

        if (takesFireDamage != null)
        {
            takesFireDamage.SetOnFire(false);
        }
    }
}
